
using WebApplication1.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace WebApplication1.Services;

public class EquipeService
{
    private readonly IMongoCollection<Equipes> equipesCollection;

    public EquipeService(
        IOptions<ProjetDatabaseSettings> DatabaseSettings)
    {
        var mongoClient = new MongoClient(
            DatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            DatabaseSettings.Value.DatabaseName);

        equipesCollection = mongoDatabase.GetCollection<Equipes>(
           name: DatabaseSettings.Value.CollectionName[0]);
    }

    public async Task<List<Equipes>> GetAsync() =>
        await equipesCollection.Find(_ => true).ToListAsync();

    public async Task<Equipes?> GetAsync(string id) =>
        await equipesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Equipes newEquipes) =>
        await equipesCollection.InsertOneAsync(newEquipes);

    public async Task UpdateAsync(string id, Equipes updatedEquipes) =>
        await equipesCollection.ReplaceOneAsync(x => x.Id == id, updatedEquipes);

    public async Task RemoveAsync(string id) =>
        await equipesCollection.DeleteOneAsync(x => x.Id == id);


}
   