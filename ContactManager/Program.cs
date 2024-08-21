using ContactManager.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "Database", "contacts.db");
//SQLITE DB PATH
builder.Services.AddDbContext<ContactContext>(options => options.UseSqlite($"Data Source={dbPath}"));

//CONNECTED TO MYSQL DB ON FREESQLDATABASE.COM
//builder.Services.AddDbContext<ContactContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("MySqlDev")));

//CONNECTED TO SQLSERVER DATABASE ON SOMEE.COM
//builder.Services.AddDbContext<ContactContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDev")));

//CORS
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
            policy.WithOrigins("https://contactmanager-4635b.web.app").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
    }
});



//MAIN RUN METHOD
var app = builder.Build();




app.UseCors("AngularApplication");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
