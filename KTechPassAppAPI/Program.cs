using KTechPassApp.Data.Entity;
using KTechPassApp.Services.Services;
using KTechPassApp.ViewModels.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ConString");
builder.Services.AddDbContextFactory<PassContext>(x=>x.UseSqlServer(connectionString),ServiceLifetime.Singleton);
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRecordService, RecordService>();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               })
               .AddJwtBearer(cfg =>
               {
                   cfg.RequireHttpsMetadata = false;
                   cfg.SaveToken = true;
                   cfg.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = configuration["Jwt:Issuer"],
                       ValidAudience = configuration["Jwt:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                   };
               });
builder.Services.AddSwaggerGen(options=>
{ 
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "KTECHMedical API",
        Description = "KTECH Medical API Development .NET 6",
        TermsOfService = new Uri("https://yusufkorucu.com/"),
        Contact = new OpenApiContact
        {
            Name = "KORUCU",
            Url = new Uri("https://kaygusuztech.com/"),
            Email="baskan@kaygusuztech.com"
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
});

builder.Services.AddAuthorization();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});

app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", () => "Hello World!")
    .ExcludeFromDescription();

app.MapControllers();

app.Run();
