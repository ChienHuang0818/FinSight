using Microsoft.EntityFrameworkCore;
using api.Data; 
using api.Interfaces;
using api.Repositories;
using Newtonsoft.Json;


var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// connect to database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger (Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositories
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

var app = builder.Build();

// Enable Swagger in all envs (easier while learning)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Map controller routes (without this, Swagger looks empty/404)
app.MapControllers();

app.Run();
