using Crosscutting.Common.Data;
using Crosscutting.Domain.Interfaces.Repositories;
using Crosscutting.Domain.Model;
using Crosscutting.Infrastructure.Contexts.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Crosscutting.Infrastructure.Repositories
{
    public abstract class MongoRepository<TEntity> : IMongoDbRepository<TEntity> where TEntity : Entity
    {
        protected readonly IMongoDbContext _context;
        protected readonly IMongoCollection<TEntity> _collection;
        protected readonly IMongoCollection<BsonDocument> _collectionBson;

        protected MongoRepository(string collectionName, IMongoDbContext context)
        {
            _context = context;
            _collection = _context.GetCollection<TEntity>(collectionName);
            _collectionBson = _context.GetCollection<BsonDocument>(collectionName);
        }

        public virtual async Task<PagedList<TEntity>> GetAllPagedAsync(Restriction restriction, Order order, Page page,
            Expression<Func<TEntity, bool>> filter = null,
            Func<Expression<Func<TEntity, bool>>, PagedList<TEntity>> whenNoExists = null,
            ProjectionDefinition<TEntity, TEntity> projection = null)
        {
            var cursor = filter == null ? _collection.Find(t => true) : _collection.Find(filter);

            if (projection != null)
                cursor = cursor.Project(projection);

            var result = await cursor.ToListAsync();

            return result.AsQueryable().Order(order).Paged(page, order, restriction);
        }

        public virtual async Task<List<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<Expression<Func<TEntity, bool>>, List<TEntity>> whenNoExists = null,
            ProjectionDefinition<TEntity, TEntity> projection = null)
        {
            var cursor = filter == null ? _collection.Find(t => true) : _collection.Find(filter);

            if (projection != null)
            {
                cursor = cursor.Project(projection);
            }

            var result = await cursor.ToListAsync();

            if (!result.Any() && whenNoExists != null) return whenNoExists(filter);

            return result;
        }

        public virtual async Task<List<TEntity>> GetAllByCommandAsync(string mongoDbTextCommand)
        {
            var document = new BsonDocument()
            {
                 { "eval",  mongoDbTextCommand }
            };

            var command = new BsonDocumentCommand<BsonDocument>(document);
            var response = await Task.Run(() => _context.Database.RunCommand(command));

            return response.ToList() as List<TEntity>;
        }

        public async Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<Expression<Func<TEntity, bool>>, TEntity> whenNoExists = null)
        {
            var cursor = await _collection.FindAsync(filter);

            var result = await cursor.FirstOrDefaultAsync();

            if (result == null && whenNoExists != null) return whenNoExists(filter);

            return result;
        }

        public async Task<TEntity> GetLastOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<Expression<Func<TEntity, bool>>, TEntity> whenNoExists = null)
        {
            var sort = Builders<TEntity>.Sort.Descending(x => x.DataCriacao);
            var retorn = await _collection.FindAsync(filter);
            if (retorn.FirstOrDefault() != null)
            {
                var result = await _collection.Find(filter).Sort(sort).FirstAsync();
                if (result == null && whenNoExists != null) return whenNoExists(filter);

                return result;
            }
            return null;
        }

        public virtual Task AddAsync(TEntity newEntity)
        {
            return _collection.InsertOneAsync(newEntity);
        }

        public virtual Task UpdateAsync(string key, TEntity newEntity) => UpdateAsync(Guid.Parse(key), newEntity);

        public virtual Task UpdateAsync(Guid key, TEntity newEntity)
        {
            return _collection.ReplaceOneAsync(Builders<TEntity>.Filter.Where(x => x.Id == key), newEntity);
        }

        public virtual Task RemoveAsync(string key) => RemoveAsync(Guid.Parse(key));

        public virtual Task RemoveAsync(Guid key)
        {
            return _collection.DeleteOneAsync(Builders<TEntity>.Filter.Where(x => x.Id == key));
        }

        public virtual Task<TEntity> GetByIdAsync(Guid id) => GetOneAsync(t => t.Id == id);

        public virtual async Task<bool> ExistsByExpressionAsync(Expression<Func<TEntity, bool>> expression)
        {
            var reg = await GetOneAsync(expression);

            return reg != null;
        }

        public virtual async Task<long> CountAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<Expression<Func<TEntity, bool>>, List<TEntity>> whenNoExists = null,
            ProjectionDefinition<TEntity, TEntity> projection = null)
        {
            var cursor = filter == null ? _collection.Find(t => true) : _collection.Find(filter);

            if (projection != null)
            {
                cursor = cursor.Project(projection);
            }

            return await cursor.CountDocumentsAsync();
        }

        public virtual Task<IClientSessionHandle> StartTransactionAsync()
        {
            return _context.Client.StartSessionAsync();
        }
    }
}