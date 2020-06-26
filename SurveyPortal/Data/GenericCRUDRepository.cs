using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SurveyPortal.Data.Entities;
using SurveyPortal.Services;

namespace SurveyPortal.Data
{
    public interface IGenericCrudRepository<TEntity, in TUpdateRequest, in TCreateRequest>
        where TEntity : class, IEntity 
        where TUpdateRequest : class
        where TCreateRequest : class
    {
        List<TEntity> GetAll();
        TEntity GetById(Guid id);
        TEntity Create(TCreateRequest item);
        void Update(Guid id, TUpdateRequest request);
        bool Delete(Guid id);
    }

    public class GenericCrudRepository<TEntity, TUpdateRequest, TCreateRequest> : IGenericCrudRepository<TEntity, TUpdateRequest, TCreateRequest> where TEntity : class, IEntity, new()
        where TUpdateRequest : class
        where TCreateRequest : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<TEntity> _set;

        public GenericCrudRepository(AppDbContext dbContext, DbSet<TEntity> set)
        {
            _dbContext = dbContext;
            _set = set;
        }

        public List<TEntity> GetAll()
        {
            return _set.ToList();
        }

        public TEntity GetById(Guid id)
        {
            return _set.FirstOrDefault(x => x.Id == id);
        }

        public TEntity Create(TCreateRequest item)
        {
            Guid id = Guid.NewGuid();

            TEntity entity = new TEntity();

            if (item is TEntity)
            {
                entity = item as TEntity;
            }
            else
            {
                PropertyCopy.Copy(item, entity);
            }

            entity.Id = id;

            _set.Add(entity);

            _dbContext.SaveChanges();

            return _set.FirstOrDefault();
        }

        public void Update(Guid id, TUpdateRequest request)
        {
            var item = _set.Single(x => x.Id == id);

            PropertyCopy.Copy(request, item);

            _dbContext.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            try
            {
                var item = _set.First(x => x.Id == id);

                _set.Remove(item);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
