

using BussenisLogicLayer;
using BussenisLogicLayer.Interface;
using Dapper;
using DataAccessLayer;
using DataAccessLayer.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Text;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

var builder = WebApplication.CreateBuilder(args);


SqlMapper.AddTypeHandler(new MySqlGuidTypeHandler());
SqlMapper.RemoveTypeMap(typeof(Guid));
SqlMapper.RemoveTypeMap(typeof(Guid?));

// Add services to the container.
builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var jwtConfig = builder.Configuration.GetSection("JwtConfig");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtConfig["Issuer"],
        ValidAudience = jwtConfig["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// injection
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepo, ProductRepo>();
builder.Services.AddTransient<IUserRepo, UserRepo>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICartRepo, CartRepo>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<IAddressService, AddressService>();
builder.Services.AddTransient<IAddressRepo, AddressRepo>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IOrderRepo, OrderRepo>();
builder.Services.AddTransient<IHistorydeliveryService, HistorydeliveryService>();
builder.Services.AddTransient<IHistorydeliveryRepo, HistorydeliveryRepo>();
builder.Services.AddTransient<IStaffService, StaffService>();
builder.Services.AddTransient<IStaffRepo, StaffRepo>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "quangdemoAPI V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
// Enable CORS
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();
IConfiguration configuration = app.Configuration;
IWebHostEnvironment environment = app.Environment;
app.MapControllers();

app.Run();
