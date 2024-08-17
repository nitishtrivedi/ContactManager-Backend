using ContactManager.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "Database", "contacts.db");
builder.Services.AddDbContext<ContactContext>(options => options.UseSqlite($"Data Source={dbPath}"));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApplication", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});


var app = builder.Build();


app.UseCors("AngularApplication");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
