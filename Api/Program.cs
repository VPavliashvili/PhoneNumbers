using Api.Configuration;
using Application.Abstractions;
using Domain.Abstractions;
using Infrastructure.Repositories;
using Infrastructure.Services;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;

builder.Services.AddControllers()
        .AddApplicationPart(presentationAssembly);

var applicationAssembly = typeof(Application.AssemblyReference).Assembly;

builder.Services.AddMediatR(applicationAssembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPasswordSecurityService, PasswordSecurityService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.ConfigureOptions<ConnectionStringsOptionsSetup>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
