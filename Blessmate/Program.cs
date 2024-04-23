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
using Microsoft.IdentityModel.Tokens;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

string?  ConnectionString = builder.Configuration.GetConnectionString("Production");
builder.Services.AddDbContext<AppDbContext>(app => app.UseNpgsql(ConnectionString));

builder.Services.AddIdentity<ApplicationUser,IdentityRole<int>>()
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication( options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwtOptions => {
    jwtOptions.RequireHttpsMetadata = true;
    jwtOptions.SaveToken = false;
    jwtOptions.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:JWTKey"]))
    };
});

builder.AddSerilogExtension();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSetting"));

builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));

builder.Services.Configure<CloundinarySettings>(builder.Configuration.GetSection("Cloundinary"));


builder.Services.AddScopedServices();

builder.Services.AddSignalR();

var app = builder.Build();

StripeConfiguration.ApiKey = app.Configuration["Stripe:SecretKey"];

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.UseCors(op => {
     op.WithOrigins("http://localhost:5000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});


app.MapHub<MessageHub>("/LiveChat");

app.Run();

// Helper Methods

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