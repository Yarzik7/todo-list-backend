using MongoDB.Bson.Serialization.Attributes;

namespace Models;

public class UpdateTaskModel {
    [BsonElement("caption")]
    public string? Caption { get; set; }

    [BsonElement("isCompleted")]
    public bool? IsCompleted { get; set; }
}