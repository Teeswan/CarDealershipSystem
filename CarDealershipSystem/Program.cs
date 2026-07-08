using CarDealershipSystem.Database.AppDbContextModels;
using CarDealershipSystem.Domain.Features.CarBrands;
using CarDealershipSystem.Domain.Features.Cars;
using CarDealershipSystem.Domain.Features.Categories;
using CarDealershipSystem.Domain.Features.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<CategoriesService>();
builder.Services.AddScoped<CarBrandsService>();
builder.Services.AddScoped<FeaturesService>();
builder.Services.AddScoped<CarsService>();

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
