using WebAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace WebAPI.Services;

public class ViviendasService
{
    private readonly IMongoCollection<Vivienda> viviendasCollection;

    public ViviendasService(
        IOptions<iwebDatabaseSettings> iwebDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            iwebDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            iwebDatabaseSettings.Value.DatabaseName);

        viviendasCollection = mongoDatabase.GetCollection<Vivienda>(
            iwebDatabaseSettings.Value.ViviendasCollectionName);
    }

    public async Task<List<Vivienda>> GetViviendas() =>
        await viviendasCollection.Find(_ => true).ToListAsync();

    public async Task<Vivienda?> GetViviendaById(Guid id) =>
        await viviendasCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<List<Vivienda>> GetViviendasByLocalidad(string localidad) =>
        await viviendasCollection.Find(x => x.Localidad == localidad).ToListAsync();

    public async Task<List<Vivienda>> GetViviendasByPropietario(Guid id) =>
        await viviendasCollection.Find(x => x.Propietario == id).ToListAsync();

    public async Task CreateVivienda(Vivienda newVivienda) =>
        await viviendasCollection.InsertOneAsync(newVivienda);

    public async Task UpdateVivienda(Guid id, Vivienda updatedVivienda) =>
        await viviendasCollection.ReplaceOneAsync(x => x.Id == id, updatedVivienda);

    public async Task RemoveVivienda(Guid id) =>
        await viviendasCollection.DeleteOneAsync(x => x.Id == id);
}