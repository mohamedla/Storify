// Copyright 2023 Mohamed Ashraf Tolba
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using StorifyAPI.Context;
using StorifyAPI.Models.Employee;
using System.Net;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StorifyContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Storify") ?? throw new InvalidOperationException("Can't found Storify Connection String While Working With Identity.")));

builder.Services.AddDefaultIdentity<StoreUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityContext>();

#region Identity Service
builder.Services.AddDbContext<IdentityContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Storify") ?? throw new InvalidOperationException("Can't found Storify Connection String While Working With Identity.")));
//builder.Services.AddIdentity<StoreUser, IdentityRole>()
//    .AddEntityFrameworkStores<IdentityContext>()
//    .AddDefaultTokenProviders(); 
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.UseAuthentication();;

//Authentification and Authorization middleware
app.UseAuthorization();
app.UseAuthorization();



app.Run();
