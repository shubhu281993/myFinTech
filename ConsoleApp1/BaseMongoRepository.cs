using Core;
using Core.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data
{
    public class BaseMongoRepository<TEntity>
        : IRepository<TEntity, string> where TEntity : IEntity
    {
        public virtual IMongoCollection<TEntity> Collection { get; set; }

        /// <summary>
        /// Queries a collection by specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>[TODO] Need to remove as GetById already exists</returns>
        public virtual FilterDefinition<TEntity> QueryId(string id)
        {
            return Builders<TEntity>.Filter.Eq(e => e.Id, id);
        }

        /// <summary>
        /// Queries a collection by specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetById(string id)
        {
            var item = Collection.Find(x => x.Id.Equals(id)).ToListAsync();

            return item.Result.FirstOrDefault();
        }

        /// <summary>
        /// Gets all items from a collection
        /// </summary>
        /// <returns>[TODO] Need to remove this method as FindAll exists</returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            var items = Collection.Find(new BsonDocument()).ToListAsync();

            return items.Result;
        }

        /// <summary>
        /// Finds a collection based on given filter
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Find(FilterDefinition<TEntity> query)
        {
            var item = Collection.Find(query).ToListAsync();

            return item.Result;
        }

        /// <summary>
        /// Finds a collection based on given filter
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual ICollection<TEntity> FindByFilter(FilterDefinition<TEntity> query)
        {
            var item = Collection.Find(query).ToListAsync();

            return item.Result;
        }

        /// <summary>
        /// Finds a collection based on given filter and projectios fields required
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="projectionDefinition"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public virtual IEnumerable<BsonDocument> Find(FilterDefinition<TEntity> filter, ProjectionDefinition<TEntity> projectionDefinition, int limit = 0)
        {
            Task<List<BsonDocument>> item = null;

            if (limit > 0)
                item = Collection.Find(filter).Project(projectionDefinition).Limit(limit).ToListAsync();
            else
                item = Collection.Find(filter).Project(projectionDefinition).ToListAsync();

            return item.Result;
        }

        /// <summary>
        /// Finds a collection based on given filter and Limit
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public virtual ICollection<TEntity> FindAndLimit(FilterDefinition<TEntity> filter, int limit)
        {
            var item = Collection.Find(filter).Limit(limit).ToListAsync();
            return item.Result;
        }

        /// <summary>
        /// Finds a collection based on given filter, projections and sort order
        /// of fields required
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="projectionDefinition"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public virtual IEnumerable<BsonDocument> FindAndSort(FilterDefinition<TEntity> filter, ProjectionDefinition<TEntity> projectionDefinition, SortDefinition<TEntity> sortOrder)
        {
            var item = Collection.Find(filter).Project(projectionDefinition).Sort(sortOrder).ToListAsync();
            return item.Result;
        }

        /// <summary>
        /// Gets all items from collection
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual ICollection<TEntity> FindAll(
            Expression<Func<TEntity, bool>> predicate)
        {
            var items = Collection.Find(predicate).ToListAsync();
            return items.Result;
        }

        /// <summary>
        /// Inserts one document
        /// </summary>
        /// <param name="item"></param>
        public virtual void InsertOne(TEntity item) => Collection.InsertOne(item);

        /// <summary>
        /// Inserts one document asynchrnously
        /// </summary>
        /// <param name="item"></param>
        public virtual void InsertOneAsync(TEntity item) => Collection.InsertOneAsync(item);

        /// <summary>
        /// This method inserts collection of items at a time
        /// </summary>
        /// <param name="items"></param>
        public virtual void InsertMany(List<TEntity> items) => Collection.InsertMany(items);

        /// <summary>
        /// Replaces a document based on filter and replacement item
        /// </summary>
        public virtual bool ReplaceOne(FilterDefinition<TEntity> query, TEntity item)
        {
            var result = Collection.ReplaceOne(query, item);

            return result.IsModifiedCountAvailable && result.ModifiedCount > 0;
        }

        /// <summary>
        /// Replaces asynchronously a document based on filter and replacement item
        /// </summary>
        public virtual bool ReplaceOneAsync(FilterDefinition<TEntity> query, TEntity item)
        {
            var result = Collection.ReplaceOneAsync(query, item);

            return result.Result.IsModifiedCountAvailable && result.Result.ModifiedCount > 0;
        }

        /// <summary>
        /// Replaces a document based on filter and replacement item
        /// </summary>
        public virtual bool UpdateOne(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            var result = Collection.UpdateOne(filter, update);
            return result.IsModifiedCountAvailable && result.ModifiedCount == 1;
        }

        /// <summary>
        /// Replaces a document based on filter and replacement item asynchrnously
        /// </summary>
        public virtual bool UpdateOneAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            var result = Collection.UpdateOneAsync(filter, update);
            return result.Result.IsModifiedCountAvailable && result.Result.ModifiedCount == 1;
        }

        /// <summary>
        /// Replaces a document based on filter and replacement item asynchrnously
        /// </summary>
        public virtual bool UpdateManyAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            var result = Collection.UpdateManyAsync(filter, update);
            return result.Result.IsModifiedCountAvailable;
        }

        /// <summary>
        /// Deletes one item based on filter
        /// </summary>
        public virtual void DeleteOne(FilterDefinition<TEntity> deleteFilter) => Collection.DeleteOne(deleteFilter);

        /// <summary>
        /// Deletes one item based on filter asynchrnously
        /// </summary>
        public virtual void DeleteOneAsync(FilterDefinition<TEntity> deleteFilter) => Collection.DeleteOneAsync(deleteFilter);

        /// <summary>
        /// Deletes many items based on filter
        /// </summary>
        public virtual void DeleteMany(FilterDefinition<TEntity> deleteFilter) => Collection.DeleteManyAsync(deleteFilter);

        /// <summary>
        /// Deletes many items based on filter asynchrnously
        /// </summary>
        public virtual void DeleteManyAsync(FilterDefinition<TEntity> deleteFilter) => Collection.DeleteManyAsync(deleteFilter);

        /// <summary>
        /// Finds based on filter and replaces with given document
        /// </summary>
        public virtual TEntity FindOneAndUpdate(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, ReturnDocument option)
        {
            var item = Collection.FindOneAndUpdate(
                filter,
                update,
                new FindOneAndUpdateOptions<TEntity>
                {
                    ReturnDocument = option
                }
            );

            return item;
        }

        /// <summary>
        /// Automic opertation - Find one and delete
        /// </summary>
        public virtual TEntity FindOneAndDelete(FilterDefinition<TEntity> filter)
        {
            var item = Collection.FindOneAndDelete(filter);

            return item;
        }

        /// <summary>
        /// Finds based on filter and updates with given document. If not found inserts the document
        /// </summary>
        public virtual void FindOneAndReplace(FilterDefinition<TEntity> updateFilter, TEntity item)
        {
            Collection.FindOneAndReplace(
                updateFilter,
                item,
                new FindOneAndReplaceOptions<TEntity>
                {
                    IsUpsert = true
                }
            );
        }

        /// <summary>
        /// Removes one document asynchrnously for a given id
        /// </summary>
        /// <remarks>[TODO] Need to remove this method as we already have DeleteOneAsync</remarks>
        public virtual bool Remove(string id)
        {
            var result = Collection.DeleteOneAsync(x => x.Id.Equals(id));

            return result.Result.DeletedCount > 0;
        }

        /// <summary>
        /// Aggregates for a given filter and performs unwind operations to create redundant data around arrays
        /// </summary>
		public virtual IList<BsonDocument> Aggregate(BsonDocument match, FilterDefinition<BsonDocument> filter, BsonDocument group, string unwind)
        {
            return Collection.Aggregate()
                .Match(match)
                .Unwind(unwind)
                .Match(filter)
                .Group(group).ToList();
        }

        /// <summary>
        /// This method creates a monitor around collection changes based on given inpupt parameters
        /// </summary>
        /// <returns></returns>		
        public IEnumerator<ChangeStreamDocument<TEntity>> Watch(ChangeStreamOptions options, PipelineDefinition<ChangeStreamDocument<TEntity>, ChangeStreamDocument<TEntity>> pipeline = null)
        {

            if (pipeline == null)
                return Collection.Watch(options).ToEnumerable().GetEnumerator();
            else
                return Collection.Watch(pipeline, options).ToEnumerable().GetEnumerator();
        }
    }
}
