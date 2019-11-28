using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Interface
{
    public interface IRepository<TEntity, in TKey> where TEntity : IEntity<TKey>
    {

        IEnumerable<TEntity> GetAll();

        TEntity GetById(string id);

        IEnumerable<TEntity> Find(FilterDefinition<TEntity> query);

        ICollection<TEntity> FindByFilter(FilterDefinition<TEntity> query);

        IEnumerable<BsonDocument> Find(FilterDefinition<TEntity> filter, ProjectionDefinition<TEntity> projectionDefinition, int limit = 0);

        ICollection<TEntity> FindAndLimit(FilterDefinition<TEntity> filter, int limit);

        IEnumerable<BsonDocument> FindAndSort(FilterDefinition<TEntity> filter, ProjectionDefinition<TEntity> projectionDefinition, SortDefinition<TEntity> sortOrder);

        void InsertOne(TEntity item);

        void InsertMany(List<TEntity> items);

        void InsertOneAsync(TEntity item);

        void DeleteOne(FilterDefinition<TEntity> query);

        void DeleteOneAsync(FilterDefinition<TEntity> query);

        void DeleteMany(FilterDefinition<TEntity> query);

        void DeleteManyAsync(FilterDefinition<TEntity> query);

        bool ReplaceOneAsync(FilterDefinition<TEntity> query, TEntity item);

        bool UpdateOne(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update);

        bool UpdateManyAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update);

        TEntity FindOneAndUpdate(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, ReturnDocument option);

        bool Remove(string id);

        ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);

        FilterDefinition<TEntity> QueryId(string id);

        IList<BsonDocument> Aggregate(BsonDocument match, FilterDefinition<BsonDocument> filter, BsonDocument group,
            string unwind);

        void FindOneAndReplace(FilterDefinition<TEntity> updateFilter, TEntity entity);

        TEntity FindOneAndDelete(FilterDefinition<TEntity> filter);

        IEnumerator<ChangeStreamDocument<TEntity>> Watch(ChangeStreamOptions options, PipelineDefinition<ChangeStreamDocument<TEntity>, ChangeStreamDocument<TEntity>> pipeline = null);
    }
}
