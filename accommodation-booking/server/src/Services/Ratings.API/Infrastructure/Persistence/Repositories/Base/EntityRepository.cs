﻿using Microsoft.EntityFrameworkCore;
using Ratings.API.Infrastructure.Persistence;
using Ratings.Models;

namespace Ratings.API.Infrasructure.Base
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : BaseEntityModel
    {
        protected readonly RatingsDbContext _dbContext;

        public EntityRepository(RatingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual List<TEntity> GetAll() => _dbContext.Set<TEntity>().ToList();

        public virtual TEntity GetById(Guid id) => _dbContext.Set<TEntity>().FirstOrDefault(e => e.Id == id);

        public virtual TEntity Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public virtual void Update(TEntity entity)
        {
            _dbContext.Entry<TEntity>(entity).State = EntityState.Modified;
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public virtual void Delete(Guid id)
        {
            var entity = GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

    }
}
