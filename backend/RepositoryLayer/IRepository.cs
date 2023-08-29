using DomainLayer.Models;

namespace RepositoryLayer
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();

        T? Get(int id);

        T Insert(T entity);

        T Update(T entity);

        void Delete(int entityId);

        void SaveChanges();

        public T? GetWithLinkedEntities(int id, string navPath);

        IEnumerable<T> GetAllWithLinkedEntities(string navPath);
    }
}
