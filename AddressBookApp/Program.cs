using BusinessLayer.Mapping;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using System;
using FluentValidation;
using FluentValidation.AspNetCore;
using BusinessLayer.Validations;
using ModelLayer.DTO;

var builder = WebApplication.CreateBuilder(args);

// Database Connection
var connectionString = builder.Configuration.GetConnectionString("AddressBookAppDB");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Register Validators Manually
builder.Services.AddValidatorsFromAssemblyContaining<AddressBookValidator>();
// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AddressBookMappingProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();