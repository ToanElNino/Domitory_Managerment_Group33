using Microsoft.EntityFrameworkCore;
using Project.Domain.Infastructure;
using Project.Infastructure.Repositories;
using MediatR;
using App.Shared.Uow;
using Project.Infastructure.Context.Student;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//Context
builder.Services.AddDbContext<StudentContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
//Repository DI
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

builder.Services.AddScoped<IMaxUnitOfWork, UnitOfWork>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

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
