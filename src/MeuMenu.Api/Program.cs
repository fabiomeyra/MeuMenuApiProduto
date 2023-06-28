using MeuMenu.Api.Configurations;
using MeuMenu.Api.Middlewares;
using MeuMenu.Application.AutoMapper;
using MeuMenu.Infra.CrossCutting.AppSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddDbContextConfiguration(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerConfiguration();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddDependencyInjectionConfiguration();

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var configuracao = appSettingsSection.Get<AppSettings>();

builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = configuracao?.Jwt?.Issuer,
        ValidateAudience = true,
        ValidAudience = configuracao?.Jwt?.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = configuracao?.Jwt?.RetornaKey(),
        ValidateLifetime = true,
        ClockSkew = configuracao!.Jwt!.ExpiresSpan
    };
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseCors();

// Adicionando Middleware para tratar exceções
app.UseExceptionHandlerMiddleware();

var options = new RewriteOptions();
options.AddRedirect("^$", "swagger");
app.UseRewriter(options);

app.UseMiddleware<RequestTelemetryMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
