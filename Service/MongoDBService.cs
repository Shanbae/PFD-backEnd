using MongoDB.Driver;

namespace LoginAPI.Service
{
    public class MongoDBService
    {

        private readonly IMongoDatabase _database;

        public MongoDBService(IConfiguration configuration)
        {
            var client=new MongoClient(configuration["MongoDB:ConnectionString"]);
            _database = client.GetDatabase(configuration["MongoDB:Databasename"]);

        }

        public IMongoCollection<T> GetCollection<T>(string collectionname)
        {

            return _database.GetCollection<T>(collectionname);


        }
    }
}
