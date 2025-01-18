using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Data.Models;

namespace Data.Context
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<AccessToken> AccessTokens
        {
            get
            {
                return _database.GetCollection<AccessToken>("MyDataModels");
            }
        }
    }
}
