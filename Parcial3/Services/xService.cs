using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Parcial3.Models;

namespace Parcial3.Services
{
    public class xService
    {
        private readonly IMongoCollection<x> xCollection;

        public xService(
            IOptions<iwebDatabaseSettings> iwebDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                iwebDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                iwebDatabaseSettings.Value.DatabaseName);

            xCollection = mongoDatabase.GetCollection<x>(
                iwebDatabaseSettings.Value.xCollectionName);
        }

        public async Task<List<x>> Getx() =>
            await xCollection.Find(_ => true).ToListAsync();

        public async Task<x?> GetxById(Guid id) =>
            await xCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task Createx(x newx) =>
            await xCollection.InsertOneAsync(newx);

        public async Task Updatex(Guid id, x updatedx) =>
            await xCollection.ReplaceOneAsync(x => x.Id == id, updatedx);

        public async Task Removex(Guid id) =>
            await xCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<x?> GetByName(string name) =>
            await xCollection.Find(x => x.Name == name).FirstOrDefaultAsync();
    }
}
