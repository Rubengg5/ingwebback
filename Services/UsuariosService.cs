using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class UsuariosService
    {
        private readonly IMongoCollection<Usuario> usuariosCollection;

        public UsuariosService(
            IOptions<iwebDatabaseSettings> iwebDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                iwebDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                iwebDatabaseSettings.Value.DatabaseName);

            usuariosCollection = mongoDatabase.GetCollection<Usuario>(
                iwebDatabaseSettings.Value.UsuariosCollectionName);
        }

        public async Task<List<Usuario>> GetUsuarios() =>
            await usuariosCollection.Find(_ => true).ToListAsync();

        public async Task<Usuario?> GetUsuarioById(Guid id) =>
        await usuariosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Usuario?> GetUsuarioByNombre(string nombre) =>
        await usuariosCollection.Find(x => x.UserName == nombre).FirstOrDefaultAsync();
        public async Task<Usuario?> GetUsuarioByEmail(string email) =>
        await usuariosCollection.Find(x => x.Email == email).FirstOrDefaultAsync();

        public async Task CreateUsuario(Usuario usuario) =>
            await usuariosCollection.InsertOneAsync(usuario);

        public async Task UpdateUsuario(Guid id, Usuario usuario) =>
            await usuariosCollection.ReplaceOneAsync(x => x.Id == id, usuario);

        public async Task RemoveUsuario(Guid id) =>
            await usuariosCollection.DeleteOneAsync(x => x.Id == id);
    }
}
