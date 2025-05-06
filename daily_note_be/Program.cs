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
builder.Services.AddDbContext<DailyNoteDbContext>(context =>
{
    context.UseSqlServer(builder.Configuration.GetConnectionString("Db_Conn"));
});

builder.Services.AddScoped<IValidator<LoginViewModel>, LoginValidator>();
builder.Services.AddScoped<IValidator<string>, EmailValidator>();
builder.Services.AddScoped<IValidator<RegisterViewModel>, RegisterValidator>();
builder.Services.AddScoped<IValidator<UpdateProfileViewModel>, UpdateProfileValidator>();
builder.Services.AddScoped<IValidator<AccountViewModel>, OtpValidator>();


builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
    )
);
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer("Bearer", opt =>
{
    var confuguration = builder.Configuration;
    Console.WriteLine(confuguration["JWT:Secret"]);
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = confuguration["JWT:Issuer"],
        ValidAudience = confuguration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(confuguration["JWT:Secret"]))
    };
    opt.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.Response.OnStarting(async () =>
            {
                ResponseResult result = new ResponseResult();
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

app.UseHttpsRedirection();

app.UseStatusCodePages();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();


