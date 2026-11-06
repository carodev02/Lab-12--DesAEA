using Microsoft.EntityFrameworkCore;
using Lab12Chumbirayco.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<VetCareDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("VetCareDb")
        ?? throw new InvalidOperationException("Connection string 'VetCareDb' is not configured.")));

var allowedOrigin = builder.Configuration["Cors:AllowedOrigin"] ?? "https://localhost:7001";
builder.Services.AddCors(options => options.AddPolicy("ApiClient", policy =>
    policy.WithOrigins(allowedOrigin).AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.InjectStylesheet("/swagger-ui/vetcare.css");
    options.DocumentTitle = "Lab 12-Chumbirayco | VetCare API";
});
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("ApiClient");
app.MapControllers();
app.Run();
