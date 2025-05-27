using System.Net.Mime;
using System.Reflection;
using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Common.Exceptions;
using WebAPI.Database;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration["Database:ConnectionString"]);
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:Key"] ??
                throw new Exception("JWT:Key not provided"))),
        };
    });


builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder =>
    {
        var origins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>();

        policyBuilder
            .WithOrigins(origins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});
builder.Services.AddHttpContextAccessor();
builder.Services
    .AddRepositories()
    .AddServices()
    .AddEndpoints();
MapsterExtensions.RegisterMapping();
var app = builder.Build();
DatabaseSeeder.Seed(app.Services);

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is NotFoundException)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(exception.Message);
        }
    });
});

app.MapEndpoints();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.Run();