using Domain.Entities;

namespace Domain.Abstraction;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task Add(T rule);
    Task Delete(T rule);
    Task Update(T rule);
    Task<T> Get(int id);
    Task<IEnumerable<T>> GetAll();
}