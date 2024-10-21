using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SecurePrivacyTask.Server.Settings;

namespace SecurePrivacyTask.Server.DBContext
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase? _database;
        public MongoDBContext(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoUrl = MongoUrl.Create(mongoDbSettings.Value.ConnectionString);
            var mongoClient = new MongoClient(mongoUrl);
            _database = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        }

        public IMongoDatabase? Database { get { return _database; } }
    }
}
