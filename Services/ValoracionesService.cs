using WebAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace WebAPI.Services;

public class ValoracionesService
{
    private readonly IMongoCollection<Valoracion> valoracionesCollection;

    public ValoracionesService(
        IOptions<iwebDatabaseSettings> iwebDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            iwebDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            iwebDatabaseSettings.Value.DatabaseName);

        valoracionesCollection = mongoDatabase.GetCollection<Valoracion>(
            iwebDatabaseSettings.Value.ValoracionesCollectionName);
    }

    public async Task<List<Valoracion>> GetValoraciones() =>
        await valoracionesCollection.Find(_ => true).ToListAsync();

    public async Task<Valoracion?> GetValoracionById(Guid id) =>
        await valoracionesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<List<Valoracion>> GetValoracionesByVivienda(Guid id) =>
        await valoracionesCollection.Find(x => x.Vivienda == id).ToListAsync();

    public async Task CreateValoracion(Valoracion newValoracion) =>
        await valoracionesCollection.InsertOneAsync(newValoracion);

    public async Task UpdateValoracion(Guid id, Valoracion updatedValoracion) =>
        await valoracionesCollection.ReplaceOneAsync(x => x.Id == id, updatedValoracion);

    public async Task RemoveValoracion(Guid id) =>
        await valoracionesCollection.DeleteOneAsync(x => x.Id == id);
}