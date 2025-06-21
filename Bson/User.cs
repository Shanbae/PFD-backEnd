using MongoDB.Bson.Serialization.Attributes;

namespace LoginAPI.Bson
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("DOB")]
        public DateTime DOB { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("Valid")]
        public bool Valid { get; set; }
        
    }
}
