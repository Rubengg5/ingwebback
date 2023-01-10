using WebAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace WebAPI.Services;

public class ReservasService
{
    private readonly IMongoCollection<Reserva> reservasCollection;

    public ReservasService(
        IOptions<iwebDatabaseSettings> iwebDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            iwebDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            iwebDatabaseSettings.Value.DatabaseName);

        reservasCollection = mongoDatabase.GetCollection<Reserva>(
            iwebDatabaseSettings.Value.ReservasCollectionName);
    }

    public async Task<List<Reserva>> GetReservas() =>
        await reservasCollection.Find(_ => true).ToListAsync();

    public async Task<Reserva?> GetReservaById(Guid id) =>
        await reservasCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<List<Reserva>> GetReservasByFecha(string fechaEntrada, string fechaSalida) =>
        await reservasCollection.Find(x => x.FechaEntrada == fechaEntrada && x.FechaSalida == fechaSalida).ToListAsync();

    public async Task<List<Reserva>> GetReservasByVivienda(Guid idVivienda) =>
        await reservasCollection.Find(x => x.IdVivienda == idVivienda).ToListAsync();

    public async Task<List<Reserva>> GetReservaByInquilinoId(Guid inquilino) =>
        await reservasCollection.Find(x => x.Inquilino == inquilino).ToListAsync();

    public async Task CreateReserva(Reserva newReserva) =>
        await reservasCollection.InsertOneAsync(newReserva);

    public async Task UpdateReserva(Guid id, Reserva updatedReserva) =>
        await reservasCollection.ReplaceOneAsync(x => x.Id == id, updatedReserva);

    public async Task RemoveReserva(Guid id) =>
        await reservasCollection.DeleteOneAsync(x => x.Id == id);
}