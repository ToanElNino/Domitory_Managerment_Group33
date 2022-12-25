using Microsoft.EntityFrameworkCore;
using Project.Domain.Infastructure;
using Project.Infastructure.Repositories;
using MediatR;
using App.Shared.Uow;
using Project.Infastructure.Context.Student;
using Project.Infastructure.Context.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Project.Infastructure.Context.Room;
using Project.Infastructure.Context.SVDKP;
using Project.Infastructure.Context.SVTP;
using Project.Infastructure.Context.Building;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//Context
builder.Services.AddDbContext<StudentContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<AccountContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<RoomContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<SVDKPContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<SVTPContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<BuildingContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

//Repository DI
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<ISVDKPRepository, SVDKPRepository>();
builder.Services.AddScoped<ISVTPRepository, SVTPRepository>();
builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();



builder.Services.AddScoped<IMaxUnitOfWork, StudentUnitOfWork>();
builder.Services.AddScoped<IMaxUnitOfWork, AccountUnitOfWork>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

//authen

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeMaze_AzureSQL v1"));
}
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
