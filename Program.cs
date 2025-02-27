using Microsoft.EntityFrameworkCore;
using product_api.Middleware;
using product_api.Persistence;
using product_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProductService();

var productDb = builder.Configuration.GetConnectionString("ProductDB");
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(productDb);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();

app.Run();