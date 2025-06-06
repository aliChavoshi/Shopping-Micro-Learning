using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities;

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("created_at")] public DateTime CreatedAt { get; set; } = DateTime.Now;
    [BsonElement("updated_at")] public DateTime UpdatedAt { get; set; }
}