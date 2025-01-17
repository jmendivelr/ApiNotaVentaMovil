var builder = WebApplication.CreateBuilder(args);

//========= PRIMERO =========
var misReglasCors = "ReglasCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: misReglasCors,
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();


                      });
});
//builder.SetIsOriginAllowed(x => true)

// Add services to the container.

builder.Services.AddControllers()
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.PropertyNamingPolicy = null; // Otra opci�n es JsonNamingPolicy.Utf8
     });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction()) //IsDevelopment()
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//========= SEGUNDO =========
app.UseCors(misReglasCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
