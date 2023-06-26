
using WebApplication1.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace WebApplication1.Services;

public class JoueurService
{
    private readonly IMongoCollection<Joueurs> joueursCollection;

    public JoueurService(
        IOptions<ProjetDatabaseSettings> DatabaseSettings)
    {
        var mongoClient = new MongoClient(
            DatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            DatabaseSettings.Value.DatabaseName);

        joueursCollection = mongoDatabase.GetCollection<Joueurs>(
           name: DatabaseSettings.Value.CollectionName[1]);
    }

    public async Task<List<Joueurs>> GetAsync() =>
        await joueursCollection.Find(_ => true).ToListAsync();

    public async Task<Joueurs> GetAsync(string id) =>
        await joueursCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Joueurs newJoueurs) =>
        await joueursCollection.InsertOneAsync(newJoueurs);

    public async Task UpdateAsync(string id, Joueurs updatedJoueurs) =>
        await joueursCollection.ReplaceOneAsync(x => x.Id == id, updatedJoueurs);

    public async Task RemoveAsync(string id) =>
        await joueursCollection.DeleteOneAsync(x => x.Id == id);


}
   