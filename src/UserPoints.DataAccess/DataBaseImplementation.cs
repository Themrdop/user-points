using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace UserPoints.DataAccess
{
    /// <summary>
    /// The implementation for the DataBase
    /// </summary>
    /// <typeparam name="T">Type or the representation of the Collection</typeparam>
    public class DataBaseImplementation<T>
        where T : class
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        public DataBaseImplementation(string connectionString, string? dataBase = null, string? collection = null)
        {
            _mongoClient = new MongoClient(connectionString);
            _database = _mongoClient.GetDatabase(dataBase is null? "UserPoints" : dataBase);

            //add the ConventionPack and register the convention to avoid adding tags to models
            var pack = new ConventionPack();
            var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("IgnoreExtraElements", conventionPack, type => true);


            Values = _database.GetCollection<T>(collection is null? "Points" : collection);
        }

        public IMongoCollection<T> Values { get; }
    }
}
