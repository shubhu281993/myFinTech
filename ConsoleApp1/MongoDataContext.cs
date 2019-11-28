using Core.Entities;
using Core.Interface;
using Library.Enums;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace Data
{
    public sealed class MongoDataContext : IDataContext
    {
        public MongoDataContext(IOptions<Settings> setting)
        {

            var settings = setting.Value;

            var mongoSettings = MongoClientSettings.FromUrl(new MongoUrl(settings.MONGODB_CONNECTION));

            if (settings.MongoIsSsl)
            {
                mongoSettings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
            }

            var client = new MongoClient(mongoSettings);

            MongoDatabase = client.GetDatabase(settings.MongoDb);

            WatchListCombo = new BaseMongoRepository<WatchListCombo>
            {
                Collection = MongoDatabase.GetCollection<WatchListCombo>(MongoCollection.WatchListCombo.ToString())
            };

            
        }

        public IMongoDatabase MongoDatabase { get; }

       
        public IRepository<WatchListCombo, string> WatchListCombo { get; }
    }
}
