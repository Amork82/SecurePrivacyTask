using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SecurePrivacyTask.Server.Models;
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
            CreateIndexes();  // Imposta gli indici quando il contesto viene inizializzato
        }

        public IMongoDatabase? Database { get { return _database; } }

        private void CreateIndexes()
        {
            if (_database != null)
            {
                var usersCollection = _database.GetCollection<User>("Users");

                var indexKeysBuilder = Builders<User>.IndexKeys;

                // Composed index for multiple searching
                var compositeIndex = new CreateIndexModel<User>(
                    indexKeysBuilder
                        .Ascending(user => user.firstName)
                        .Ascending(user => user.lastName)
                        .Ascending(user => user.dateOfBirth)
                        .Ascending(user => user.isEnabled)
                );

                // Single index on FirstName
                var firstNameIndex = new CreateIndexModel<User>(
                    indexKeysBuilder.Ascending(user => user.firstName)
                );

                // Single index on su LastName
                var lastNameIndex = new CreateIndexModel<User>(
                    indexKeysBuilder.Ascending(user => user.lastName)
                );

                // Single index on DateOfBirth
                var dateOfBirthIndex = new CreateIndexModel<User>(
                    indexKeysBuilder.Ascending(user => user.dateOfBirth)
                );

                // Single index on IsEnabled
                var isEnabledIndex = new CreateIndexModel<User>(
                    indexKeysBuilder.Ascending(user => user.isEnabled)
                );

                // Load index in the collection
                usersCollection.Indexes.CreateMany(new[] { compositeIndex, firstNameIndex, lastNameIndex, dateOfBirthIndex, isEnabledIndex });
            }
        }
    }

}
