using B3serverREST.Services;
using WebAPI.Models;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<iwebDatabaseSettings>(
    builder.Configuration.GetSection("iwebDatabase"));

builder.Services.AddSingleton<ViviendasService>();
builder.Services.AddSingleton<ReservasService>();
builder.Services.AddSingleton<UsuariosService>();
builder.Services.AddSingleton<ValoracionesService>();
builder.Services.AddSingleton<MensajesService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();