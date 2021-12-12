using CleanArch.Infra.Data.Context;
using CleanArch.Infra.IoC;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
static void RegisterServices(IServiceCollection services)
{
    DependencyContainer.RegisterServices(services);
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "University Api", Version = "v1" });
});

var connectionStringUni = builder.Configuration.GetConnectionString("UniversityDBConnection");
builder.Services.AddDbContext<UniversityDBContext>(options =>
{
    options.UseSqlServer(connectionStringUni);
});

builder.Services.AddMediatR(typeof(Program));

RegisterServices(builder.Services);

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
