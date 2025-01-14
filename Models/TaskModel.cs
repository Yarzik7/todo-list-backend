namespace Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class TaskModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("caption")]
    public string Caption { get; set; } = null!;

    [BsonElement("isCompleted")]
    public bool IsCompleted { get; set; }
}