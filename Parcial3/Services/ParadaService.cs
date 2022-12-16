using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Parcial3.Models;
using System.Xml.Linq;

namespace Parcial3.Services
{
    public class ParadaService
    {
        private readonly IMongoCollection<Parada> paradaCollection;

        public ParadaService(
            IOptions<iwebDatabaseSettings> iwebDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                iwebDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                iwebDatabaseSettings.Value.DatabaseName);

            paradaCollection = mongoDatabase.GetCollection<Parada>(
                iwebDatabaseSettings.Value.ParadaCollectionName);
        }

        public async Task<List<Parada>> GetParadas() =>
            await paradaCollection.Find(_ => true).ToListAsync();

        public async Task<Parada?> GetParadaByCodParada(int id) =>
            await paradaCollection.Find(x => x.codParada == id).FirstOrDefaultAsync();

        public async Task CreateParada(Parada newx) =>
            await paradaCollection.InsertOneAsync(newx);

        public async Task CreateParadas(List<Parada> listx) =>
            await paradaCollection.InsertManyAsync(listx);

        public async Task UpdateParada(int id, Parada updatedx) =>
            await paradaCollection.ReplaceOneAsync(x => x.codParada == id, updatedx);

        public async Task RemoveParada(int id) =>
            await paradaCollection.DeleteOneAsync(x => x.codParada == id);

        public async Task<List<Parada>> GetParadasByLineaYSentido(int linea, int sentido) =>
            await paradaCollection.Find(x => x.codLinea == linea && x.sentido == sentido).ToListAsync();

        //public async Task<List<Parada>> GetByUsuario(int Usuario) =>
        //    await paradaCollection.Find(x => x.Usuario == Usuario).ToListAsync();
    }
}
