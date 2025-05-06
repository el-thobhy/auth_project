using Daily_Note.Models;
using Daily_Note.Validator;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using daily_note_be.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<DailyNoteDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Db_Conn"));
});

// Register validators
builder.Services.AddScoped<IValidator<LoginViewModel>, LoginValidator>();
builder.Services.AddScoped<IValidator<string>, EmailValidator>();
builder.Services.AddScoped<IValidator<RegisterViewModel>, RegisterValidator>();
builder.Services.AddScoped<IValidator<UpdateProfileViewModel>, UpdateProfileValidator>();
builder.Services.AddScoped<IValidator<AccountViewModel>, OtpValidator>();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );
});

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer("Bearer", options =>
{
    var configuration = builder.Configuration;
    Console.WriteLine(configuration["JWT:Secret"]);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["JWT:Issuer"],
        ValidAudience = configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.Response.OnStarting(async () =>
            {
                await context.Response.WriteAsync("You are not authorized!");
            });
            return Task.CompletedTask;
        },
        OnForbidden = context =>
        {
            context.Response.OnStarting(async () =>
            {
                await context.Response.WriteAsync("You are forbidden!");
            });
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

// Map a basic health-check endpoint
app.MapGet("/", () => "API is running");

// Middleware pipeline
app.UseHttpsRedirection();

app.UseStatusCodePages();

app.UseCors(); // <--- Pindah ke atas sebelum Auth

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
