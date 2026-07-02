using Data; // <-- your Data namespace
using Microsoft.EntityFrameworkCore; // <-- your Data namespace
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("products.db")); // Use SQLite database

var app = builder.Build();  

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.MapControllers();
app.UseAuthorization();
app.Run();

