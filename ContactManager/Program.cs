using ContactManager.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "Database", "contacts.db");
//COMMENTED OUT AS NOT USING SQLITE
//builder.Services.AddDbContext<ContactContext>(options => options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddCors(options =>
{
    var corsPolicyName = "AngularApplication";
    var isDevelopment = builder.Environment.IsDevelopment();
    if (isDevelopment)
    {
        options.AddPolicy(corsPolicyName, policy =>
        {
            policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
    } else
    {
        options.AddPolicy(corsPolicyName, policy =>
        {
            policy.WithOrigins("").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
    }
});


var app = builder.Build();


app.UseStaticFiles();

app.UseRouting();

app.UseCors("AngularApplication");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
