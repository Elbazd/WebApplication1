
using WebApplication1.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace WebApplication1.Services;

public class StadeService
{
    private readonly IMongoCollection<Stades> stadesCollection;

    public StadeService(
        IOptions<ProjetDatabaseSettings> DatabaseSettings)
    {
        var mongoClient = new MongoClient(
            DatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            DatabaseSettings.Value.DatabaseName);

        stadesCollection = mongoDatabase.GetCollection<Stades>(
           name: DatabaseSettings.Value.CollectionName[2]);
    }

    public async Task<List<Stades>> GetAsync() =>
        await stadesCollection.Find(_ => true).ToListAsync();

    public async Task<Stades?> GetAsync(string id) =>
        await stadesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Stades newStades) =>
        await stadesCollection.InsertOneAsync(newStades);

    public async Task UpdateAsync(string id, Stades updatedStades) =>
        await stadesCollection.ReplaceOneAsync(x => x.Id == id, updatedStades);

    public async Task RemoveAsync(string id) =>
        await stadesCollection.DeleteOneAsync(x => x.Id == id);

    internal Task CreateAsync(Joueurs newJoueur)
    {
        throw new NotImplementedException();
    }
}
