using System.Text;
using DanilvarKanji.Application;
using DanilvarKanji.Application.Behaviors;
using DanilvarKanji.Data.Configuration;
using DanilvarKanji.Extensions;
using DanilvarKanji.Mappings;
using DanilvarKanji.Infrastructure;
using DanilvarKanji.Infrastructure.Data;
using DanilvarKanji.OptionsSetup;
using DanilvarKanji.Shared.Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddFluentValidationAutoValidation(x => x.DisableDataAnnotationsValidation = true)
    .AddFluentValidationClientsideAdapters();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddApplicationSettings(builder.Configuration);

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
builder.Services.AddValidatorsFromAssemblyContaining<Application>();

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBeh<,>));


// Add services to the container.

//builder.Services.AddIdentityServices(builder.Configuration);


/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));*/

builder.Services.AddCors();

builder.Services.AddControllers()
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

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
        policy.WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader()
);

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();