using Blessmate.Data;
using Blessmate.Extensions;
using Blessmate.Helpers;
using Blessmate.Models;
using Blessmate.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

string? ConnectionString = builder.Configuration.GetConnectionString("Defualt");
builder.Services.AddDbContext<AppDbContext>(app => app.UseSqlite(ConnectionString));

builder.Services.AddIdentity<Therpist,IdentityRole<int>>()
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.AddSerilogExtension();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSetting"));

builder.Services.AddScopedServices();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsProduction())
    InitDatabase();

app.Run();



void InitDatabase(){
    using var scope = app.Services.CreateScope();
    var dbIntializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();
    dbIntializer.Init();
}
