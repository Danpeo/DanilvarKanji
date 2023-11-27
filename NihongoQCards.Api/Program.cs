using System.Text;
using DanilvarKanji.Application;
using DanilvarKanji.Data.Configuration;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Extensions;
using DanilvarKanji.Mappings;
using DanilvarKanji.Infrastructure;
using DanilvarKanji.Infrastructure.Data;
using DanilvarKanji.OptionsSetup;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLamarServices();

builder.Services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddFluentValidationAutoValidation(x => x.DisableDataAnnotationsValidation = true)
    .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddODataQueryFilter();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DanilvarKanji API",
        Version = "v1"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddMappings();


// Add services to the container.

//builder.Services.AddIdentityServices(builder.Configuration);


/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));*/

var modelBuilder = new ODataConventionModelBuilder();

builder.Services.AddControllers()
    .AddOData(options => options
        .Select()
        .Filter()
        .OrderBy()
        .Expand()
        .Count()
        .SetMaxTop(null)
        .AddRouteComponents("odata", modelBuilder.GetEdmModel()))
    .AddJsonOptions(options => { });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
    });

builder.Services.ConfigureOptions<JwtOptionsSetup>();

builder.Services.AddAuthorization();

/*
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
        policy.WithOrigins(/*"http://localhost:7106", "https://localhost:7106", "http://localhost:7046",
                "https://localhost:7046"*/"*")
            .AllowAnyMethod()
            .AllowAnyHeader()
    //.WithHeaders(HeaderNames.ContentType)
);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();