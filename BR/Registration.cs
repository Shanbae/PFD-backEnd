using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LoginAPI.BR
{
    public class Registration
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; }
        
        public string Email { get; set; }
     
        public DateTime DOB { get; set; }
       
        public string Password { get; set; }

        public bool Valid { get; set; }
    }
}
