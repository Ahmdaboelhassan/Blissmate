using System.Text;
using Blessmate;
using Blessmate.Data;
using Blessmate.Extensions;
using Blessmate.Helpers;
using Blessmate.Models;
using Blessmate.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

string? ConnectionString = builder.Configuration.GetConnectionString("Defualt");
builder.Services.AddDbContext<AppDbContext>(app => app.UseSqlite(ConnectionString));

builder.Services.AddIdentity<ApplicationUser,IdentityRole<int>>()
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// builder.Services.AddAuthentication( options => {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// })
// .AddJwtBearer(jwtOptions => {
//     jwtOptions.RequireHttpsMetadata = true;
//     jwtOptions.SaveToken = false;
//     jwtOptions.TokenValidationParameters = new TokenValidationParameters{
//         ValidateIssuerSigningKey = true,
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidIssuer = builder.Configuration["JWT:Issuer"],
//         ValidAudience = builder.Configuration["JWT:Audience"],
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
//     };
// });

builder.AddSerilogExtension();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSetting"));

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<CloundinarySettings>(builder.Configuration.GetSection("Cloundinary"));

builder.Services.AddScopedServices();

builder.Services.AddSignalR();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(op => {
     op.WithOrigins("http://127.0.0.1:5500")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});

if (app.Environment.IsProduction())
    InitDatabase();

app.MapHub<MessageHub>("/LiveChat");

app.Run();



void InitDatabase(){
    using var scope = app.Services.CreateScope();
    var dbIntializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();
    dbIntializer.Init();
}

void SeedFakeData(){
    using var scope = app.Services.CreateScope();
    var seedFakeData = new SeedFakeData(scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>());
    seedFakeData.Seed();
}