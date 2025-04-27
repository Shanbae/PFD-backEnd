using LoginAPI.Bson;
using MongoDB.Driver;

namespace LoginAPI.Service
{
    public class LoginService
    {
         private readonly IMongoCollection<User> _usersCollection;


        public LoginService(MongoDBService mongoDbService)
        {
            _usersCollection = mongoDbService.GetCollection<User>("Users");
        }

        public async Task<List<User>> GetUserAsync()
        {
            return await _usersCollection.Find(user=> true).ToListAsync();
        }

    }
}
