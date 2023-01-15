using B3serverREST.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebAPI.Models;

namespace B3serverREST.Services
{
    public class LogService
    {
        private readonly IMongoCollection<Log> LogCollection;

        public LogService(
            IOptions<iwebDatabaseSettings> iwebDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                iwebDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                iwebDatabaseSettings.Value.DatabaseName);

            LogCollection = mongoDatabase.GetCollection<Log>(
                iwebDatabaseSettings.Value.LogCollectionName);
        }

        public async Task<List<Log>> GetLog() =>
            await LogCollection.Find(_ => true).ToListAsync();

        //public async Task<Log?> GetLogById(Guid id) =>
        //    await LogCollection.Find(x => x.id == id).FirstOrDefaultAsync();

        public async Task CreateLog(Log newLog) =>
            await LogCollection.InsertOneAsync(newLog);

        //public async Task UpdateLog(Guid id, Log updatedLog) =>
        //    await LogCollection.ReplaceOneAsync(x => x.id == id, updatedLog);

        //public async Task RemoveLog(Guid id) =>
        //    await LogCollection.DeleteOneAsync(x => x.id == id);
    }
}
