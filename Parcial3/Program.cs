using Parcial3.Models;
using Parcial3.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<iwebDatabaseSettings>(
    builder.Configuration.GetSection("iwebDatabase"));

builder.Services.AddSingleton<UsuarioService>();
builder.Services.AddSingleton<xService>();
builder.Services.AddSingleton<ParadaService>();
/*builder.Services.AddSingleton<UsuariosService>();*/

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