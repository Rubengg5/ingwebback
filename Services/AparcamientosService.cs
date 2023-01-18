using B3serverREST.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebAPI.Models;

namespace B3serverREST.Services
{
    public class AparcamientosService
    {
        private readonly IMongoCollection<Aparcamiento> AparcamientosCollection;

        public AparcamientosService(
            IOptions<iwebDatabaseSettings> iwebDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                iwebDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                iwebDatabaseSettings.Value.DatabaseName);

            AparcamientosCollection = mongoDatabase.GetCollection<Aparcamiento>(
                iwebDatabaseSettings.Value.AparcamientosCollectionName);
        }

        public async Task<List<Aparcamiento>> GetAparcamientos() =>
            await AparcamientosCollection.Find(_ => true).ToListAsync();

        public async Task<Aparcamiento?> GetAparcamientoBypoiID(int poiID) =>
            await AparcamientosCollection.Find(x => x._id == poiID).FirstOrDefaultAsync();

        public async Task CreateAparcamiento(Aparcamiento newAparcamiento) =>
            await AparcamientosCollection.InsertOneAsync(newAparcamiento);

        public async Task UpdateAparcamiento(int poiID, Aparcamiento updatedAparcamiento) =>
            await AparcamientosCollection.ReplaceOneAsync(x => x._id == poiID, updatedAparcamiento);

        public async Task RemoveAparcamiento(int poiID) =>
            await AparcamientosCollection.DeleteOneAsync(x => x._id == poiID);

    }
}
