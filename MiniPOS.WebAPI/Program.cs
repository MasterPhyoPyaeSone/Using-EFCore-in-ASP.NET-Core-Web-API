
using Microsoft.EntityFrameworkCore;
using MiniPOS.Database.AppDbContextModels;

var builder = WebApplication.CreateBuilder(args);

// 1. Register Database Context
// Make sure "DefaultConnection" exists in your appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
    
// 2. Add services to the container.
builder.Services.AddControllers();          // ၎င်းမပါလျှင် သင်ရေးထားသော Controller များကို API အနေနဲ့ ခေါ်ယူ၍ မရနိုင်ပါ။
builder.Services.AddEndpointsApiExplorer(); // /api/Product) ကို ရှာဖွေဖော်ထုတ်ပေးသည့် စနစ်ဖြစ်သည်။
builder.Services.AddSwaggerGen();           // Generates the Swagger UI documentation
builder.Services.AddOpenApi();              // If you are using the new .NET 9+ OpenAPI support

var app = builder.Build();

// 3. Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Adds the visual UI at /swagger
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// 4. Map Controllers and Endpoints
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// 5. Run the application
app.Run(); // ONLY ONE app.Run() at the end

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
