using MongoDB.Bson.Serialization.Attributes;

namespace LoginAPI.BR
{
    public class Registration
    {
       
       

        
        public string Name { get; set; }
        
        public string Email { get; set; }
     
        public DateTime DOB { get; set; }
       
        public string Password { get; set; }
    }
}
