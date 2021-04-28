using System.Collections.Generic;

namespace Core.Interfaces.Gateways.Repositories
{
    public interface IRepository<T>
    {
        T GetById(object id);
        List<T> ListAll();
        T GetSingleBySpec(ISpecification<T> specification);
        List<T> List(ISpecification<T> specification);
        T Add(T entity);
        T Update(T entity, object id);
        void Delete(T entity);

    }
}
