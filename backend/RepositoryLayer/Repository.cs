using DomainLayer.Context;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly BlogDbContext _blogDbContext;
        private DbSet<T> entityDbSet;

        public Repository(BlogDbContext applicationDbContext)
        {
            _blogDbContext = applicationDbContext;
            entityDbSet = _blogDbContext.Set<T>();
        }   
        
        public IEnumerable<T> GetAll()
        {
            return entityDbSet.AsEnumerable();
        }

        public T? GetWithLinkedEntities(int id, string navPath)
        {
            return entityDbSet.Include(navPath).SingleOrDefault(c => c.Id == id);
        }

       

        public T? Get(int id)
        {
            return entityDbSet.SingleOrDefault(c => c.Id == id);
        }
   
        public T Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var ent = entityDbSet.Add(entity);
            _blogDbContext.SaveChanges();
            return ent.Entity;
        }

        public void Delete(int entityId)
        {
            var entity = entityDbSet.SingleOrDefault(c => c.Id == entityId);
            if (entity == null) return;

            entityDbSet.Remove(entity);
            _blogDbContext.SaveChanges();
        }
      
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entityDbSet.Update(entity);
            _blogDbContext.SaveChanges();
        }  

        public void SaveChanges()
        {
            _blogDbContext.SaveChanges();
        }
    }
}
