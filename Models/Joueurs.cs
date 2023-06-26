using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApplication1.Models
{
    public class Joueurs
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? name { get; set; }

        public string? nationalite { get; set; }

        public string? equipes { get; set; }

        public string? age { get; set; }
    }
}
