using Microsoft.EntityFrameworkCore;
using RobotAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Lägg till DbContext-tjänsten **innan** builder.Build()
builder.Services.AddDbContext<RobotsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Lägg till API Controllers
builder.Services.AddControllers();

// 3. Lägg till Swagger för API-dokumentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4. Om utvecklingsmiljö, aktivera Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 5. HTTPS redirect
app.UseHttpsRedirection();

// 6. Mappa controllers
app.MapControllers();

app.Run();
