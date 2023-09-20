// Copyright 2023 Mohamed Ashraf Tolba
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using StorifyAPI.Context;
using StorifyAPI.Models.Employee;
//using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StorifyAPI.Models.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StorifyContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Storify") ?? throw new InvalidOperationException("Can't found Storify Connection String While Working With Identity.")));

//builder.Services.AddDefaultIdentity<StoreUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<IdentityContext>();

#region Identity Service
builder.Services.AddDbContext<IdentityContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Storify") ?? throw new InvalidOperationException("Can't found Storify Connection String While Working With Identity.")));

builder.Services.AddIdentity<StoreUser, IdentityRole>(
    option =>
    {
        option.Password.RequireDigit = true;
        option.Password.RequireUppercase = true;
        option.Password.RequireLowercase = true;
        option.Password.RequireNonAlphanumeric = true;
        option.Password.RequiredLength = 8;
        option.User.RequireUniqueEmail = true;
    }
    )
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();
#endregion
#region JWT Config
var jwtSetting = builder.Configuration.GetSection("JwtSettings");
var secretKey = builder.Configuration.GetSection("Secret").Value; // better be system environment variable
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSetting.GetSection("validIssuer").Value,
        ValidAudience = jwtSetting.GetSection("validAudience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddScoped<IAuthenticationManager, AuthenticationManager>();
#endregion
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

//Authentification and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();



app.Run();
