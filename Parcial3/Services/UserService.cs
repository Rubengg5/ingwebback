using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Parcial3.Models;

namespace Parcial3.Services
{
    public class UsuarioService
    {
        private readonly IMongoCollection<Usuario> UsuariosCollection;

        public UsuarioService(
            IOptions<iwebDatabaseSettings> iwebDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                iwebDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                iwebDatabaseSettings.Value.DatabaseName);

            UsuariosCollection = mongoDatabase.GetCollection<Usuario>(
                iwebDatabaseSettings.Value.UsuarioCollectionName);
        }

        public async Task<List<Usuario>> GetUsuarios() =>
            await UsuariosCollection.Find(_ => true).ToListAsync();

        public async Task<Usuario?> GetUsuarioById(Guid id) =>
        await UsuariosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Usuario?> GetUsuarioByNombre(string nombre) =>
        await UsuariosCollection.Find(x => x.UserName == nombre).FirstOrDefaultAsync();

        public async Task CreateUsuario(Usuario Usuario) =>
            await UsuariosCollection.InsertOneAsync(Usuario);

        public async Task UpdateUsuario(Guid id, Usuario Usuario) =>
            await UsuariosCollection.ReplaceOneAsync(x => x.Id == id, Usuario);

        public async Task RemoveUsuario(Guid id) =>
            await UsuariosCollection.DeleteOneAsync(x => x.Id == id);
    }
}
