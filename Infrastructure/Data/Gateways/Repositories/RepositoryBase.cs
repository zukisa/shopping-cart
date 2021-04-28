using Core.Interfaces.Gateways.Repositories;
using Infrastructure.Data.EF;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Gateways.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : class
    {
        protected ShoppingCartDbContext _dbContext;
        private readonly DbSet<T> Table;

        public RepositoryBase(ShoppingCartDbContext dbContext)
        {
            _dbContext = dbContext;
            Table = _dbContext.Set<T>();
        }
        public T Add(T entity)
        {
            Table.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            Table.Remove(entity);
            _dbContext.SaveChanges();
        }

        public T GetById(object id)
        {
            return Table.Find(id);
        }

        public T GetSingleBySpec(ISpecification<T> specification)
        {
            return List(specification).FirstOrDefault();
        }

        public List<T> List(ISpecification<T> specification)
        {
            IQueryable<T> query = Table.Where(specification.Criteria);
            return query.ToList();
        }

        public List<T> ListAll()
        {
            return Table.ToList();
        }

        public T Update(T entity, object id)
        {
            T entityToUpdate = Table.Find(id);
            if(entityToUpdate != null)
            {
                _dbContext.Entry(entityToUpdate).CurrentValues.SetValues(entity);
                _dbContext.SaveChanges();
            }
            return entity;
        }
    }
}
