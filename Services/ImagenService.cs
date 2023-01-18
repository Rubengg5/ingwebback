using B3serverREST.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebAPI.Models;

namespace B3serverREST.Services
{
    public class ImagenService
    {
        private readonly IMongoCollection<Imagen> ImagenCollection;

        public ImagenService(
            IOptions<iwebDatabaseSettings> iwebDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                iwebDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                iwebDatabaseSettings.Value.DatabaseName);

            ImagenCollection = mongoDatabase.GetCollection<Imagen>(
                iwebDatabaseSettings.Value.ImagenCollectionName);
        }

        public async Task<List<Imagen>> GetImagenes() =>
            await ImagenCollection.Find(_ => true).ToListAsync();

        public async Task<Imagen?> GetImagenByID(int poiID) =>
            await ImagenCollection.Find(x => x._id == poiID).FirstOrDefaultAsync();

        public async Task<Imagen?> GetImagenByAparcamiento(int aparcamiento) =>
            await ImagenCollection.Find(x => x.idAparcamiento == aparcamiento).FirstOrDefaultAsync();

        public async Task CreateImagen(Imagen newImagen) =>
            await ImagenCollection.InsertOneAsync(newImagen);

        public async Task UpdateImagen(int poiID, Imagen updatedImagen) =>
            await ImagenCollection.ReplaceOneAsync(x => x._id == poiID, updatedImagen);

        public async Task RemoveImagen(int poiID) =>
            await ImagenCollection.DeleteOneAsync(x => x._id == poiID);

    }
}
