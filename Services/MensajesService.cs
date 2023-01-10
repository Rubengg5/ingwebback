using B3serverREST.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebAPI.Models;

namespace B3serverREST.Services
{
    public class MensajesService
    {
        private readonly IMongoCollection<Mensaje> MensajesCollection;

        public MensajesService(
            IOptions<iwebDatabaseSettings> iwebDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                iwebDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                iwebDatabaseSettings.Value.DatabaseName);

            MensajesCollection = mongoDatabase.GetCollection<Mensaje>(
                iwebDatabaseSettings.Value.MensajesCollectionName);
        }

        public async Task<List<Mensaje>> GetMensajes() =>
            await MensajesCollection.Find(_ => true).ToListAsync();

        public async Task<Mensaje?> GetMensajeById(Guid id) =>
            await MensajesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<Mensaje>> GetMensajesByRemitente(Guid remitente) =>
            await MensajesCollection.Find(x => x.remitente == remitente).ToListAsync();

        public async Task<List<Mensaje>> GetMensajesByDestinatario(Guid destinatario) =>
            await MensajesCollection.Find(x => x.destinatario == destinatario).ToListAsync();

        public async Task<List<Mensaje>> GetMensajesByRemitenteAndDestinatario(Guid remitente, Guid destinatario) =>
            await MensajesCollection.Find(x => (x.destinatario == destinatario && x.remitente == remitente) || (x.destinatario == remitente && x.remitente == destinatario)).ToListAsync();

        public async Task CreateMensaje(Mensaje newMensaje) =>
            await MensajesCollection.InsertOneAsync(newMensaje);

        public async Task UpdateMensaje(Guid id, Mensaje updatedMensaje) =>
            await MensajesCollection.ReplaceOneAsync(x => x.Id == id, updatedMensaje);

        public async Task RemoveMensaje(Guid id) =>
            await MensajesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
