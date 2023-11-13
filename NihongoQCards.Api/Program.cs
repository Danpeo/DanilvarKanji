using DanilvarKanji.Application.Characters.Commands;
using DanilvarKanji.Application.Characters.Handlers;
using DanilvarKanji.Data;
using DanilvarKanji.Data.Configuration;
using DanilvarKanji.Extensions;
using DanilvarKanji.Mappings;
using DanilvarKanji.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLamarServices();

builder.Services.AddFluentValidationAutoValidation(x => x.DisableDataAnnotationsValidation = true)
    .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddAutoMapper(typeof(CharacterMapperProfile));

builder.Services.AddODataQueryFilter();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DanilvarKanji API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

/*builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")))*/
;

/*builder.Services.AddDbContext<DanilvarKanji.Infrastructure.Data.ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSql")));*/

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<CreateCharacterCommand>());
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<CreateCharacterHandler>());

// Add services to the container.

builder.Services.AddIdentityServices(builder.Configuration);


/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));*/

var modelBuilder = new ODataConventionModelBuilder();
/*modelBuilder.EntityType<AppUser>();
modelBuilder.EntitySet<Character>("Characters");
modelBuilder.EntitySet<StringDefinition>("Definitions");*/

builder.Services.AddControllers()
    .AddOData(options => options
        .Select()
        .Filter()
        .OrderBy()
        .Expand()
        .Count()
        .SetMaxTop(null)
        .AddRouteComponents("odata", modelBuilder.GetEdmModel()))
    .AddJsonOptions(options =>
    {
        /*
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        */
        //options.JsonSerializerOptions.Converters.Add(new NumberToStringConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:7106", "https://localhost:7106", "http://localhost:7046",
            "https://localhost:7046")
        .AllowAnyMethod()
        .WithHeaders(HeaderNames.ContentType)
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();