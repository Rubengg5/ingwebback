using B3serverREST.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebAPI.Models;

namespace B3serverREST.Services
{
    public class MessagesService
    {
        private readonly IMongoCollection<Message> MessagesCollection;

        public MessagesService(
            IOptions<iwebDatabaseSettings> iwebDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                iwebDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                iwebDatabaseSettings.Value.DatabaseName);

            MessagesCollection = mongoDatabase.GetCollection<Message>(
                iwebDatabaseSettings.Value.MessagesCollectionName);
        }

        public async Task<List<Message>> GetMessages() =>
            await MessagesCollection.Find(_ => true).ToListAsync();

        public async Task<Message?> GetMessageById(Guid id) =>
            await MessagesCollection.Find(x => x.id == id).FirstOrDefaultAsync();

        public async Task<List<Message>> GetConversacionByRemitenteAndDestinatario(Guid idRemitente, Guid idDestinatario) =>
            await MessagesCollection.Find(x => x.de == idRemitente && x.para == idDestinatario).SortBy(x => x.stamp).ToListAsync();

        public async Task<List<Message>> GetByUserDesc(Guid id) =>
            await MessagesCollection.Find(x => x.de == id || x.para == id).SortByDescending(x => x.stamp).ToListAsync();

        //public async Task<List<Message>> GetByUserWithoutResponse(Guid id) =>
        //    await MessagesCollection.Find(x => x.para == id && x.stamp).SortByDescending(x => x.stamp).ToListAsync();

        //public async Task<List<Message>> GetContactosByUser(Guid id) =>
        //    await MessagesCollection.Find(x => x.de == remitente).SortByDescending(x => x.stamp).ToListAsync();

        public async Task CreateMessage(Message newMessage) =>
            await MessagesCollection.InsertOneAsync(newMessage);

        public async Task UpdateMessage(Guid id, Message updatedMessage) =>
            await MessagesCollection.ReplaceOneAsync(x => x.id == id, updatedMessage);

        public async Task RemoveMessage(Guid id) =>
            await MessagesCollection.DeleteOneAsync(x => x.id == id);

        //public async Task<List<Mensaje>> GetPendientes(string id) =>
        //    await MensajeCollection.Find(Mensaje => Mensaje.cabecera.de == id).ToListAsync();

        //public async Task<List<Mensaje>> IsLast(string id, DateTime stamp) =>
        //    await MensajeCollection.Find(Mensaje => Mensaje.cabecera.de == id && Mensaje.cabecera.stamp.CompareTo(stamp) > 0).ToListAsync();

        //public async Task<List<string>> GetContactos(string usuario)
        //{
        //    List<Mensaje> mensajes = await GetByUsuario(usuario);
        //    var contactos = new List<string>();
        //    foreach (var m in mensajes)
        //    {
        //        if (!contactos.Contains(m.cabecera.para))
        //        {
        //            contactos.Add(m.cabecera.para);
        //        }
        //    }

        //    return contactos;
        //}
    }
}
