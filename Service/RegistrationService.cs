using LoginAPI.BR;
using LoginAPI.Bson;
using MongoDB.Driver;

namespace LoginAPI.Service
{
    public class RegistrationService
    {
        private readonly IMongoCollection<Registration> _usersCollection;


        public RegistrationService(MongoDBService mongoDbService)
        {
            _usersCollection = mongoDbService.GetCollection<Registration>("Users");
        }

        public async Task AddUserAsync(Registration reg)
        {
             await _usersCollection.InsertOneAsync(reg);
        }

    }
}
