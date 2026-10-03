using Microsoft.EntityFrameworkCore;
using MyFirstAiChat.Data;
using MyFirstAiChat.Service;
using System;

var builder = WebApplication.CreateBuilder(args);

// CORS policy name
string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<EmbeddingService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("EmbeddingsDb"));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://yourfrontend.com")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run();