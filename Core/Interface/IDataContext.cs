using Core.Entities;
using MongoDB.Driver;
using System;

namespace Core.Interface
{
    public interface IDataContext
    {
        IMongoDatabase MongoDatabase { get; }

        IRepository<WatchListCombo, string> WatchListCombo { get; }
    }
}
