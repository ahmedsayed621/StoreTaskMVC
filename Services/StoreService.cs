using Microsoft.EntityFrameworkCore;
using StoreTaskMVC.Data;
using StoreTaskMVC.Interfaces;
using StoreTaskMVC.Models;

namespace StoreTaskMVC.Services
{
    public class StoreService : IStoreService
    {
        private readonly StoreDbContext _dbContext;

        public StoreService(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Store Create(Store obj)
        {
            

            _dbContext.Stores.Add(obj);

            
            _dbContext.SaveChanges();

            var defaultSpace = new Space
            {
                Name = "Default Space",
                StoreId = obj.Id
            };

            _dbContext.Spaces.Add(defaultSpace);
            _dbContext.SaveChanges();

            return _dbContext.Stores.OrderBy(a => a.Id).LastOrDefault();
        }

        public Store Delete(Store obj)
        {
            _dbContext.Stores.Remove(obj);
            foreach(var space in _dbContext.Spaces)
            {
                if(obj.Id== space.StoreId)
                {
                    _dbContext.Spaces.Remove(space);
                }
            }
            _dbContext.SaveChanges();
            

            return obj;
        }

        public IEnumerable<Store> Get()
        {
            //IQueryable<Store> data = GetStores();
            return _dbContext.Stores
                       .ToList();
        }

        public Store GetById(int id)
        {
            var data = _dbContext.Stores
                .Where(a => a.Id == id).SingleOrDefault();
            return data;
        }

        

        public Store Update(Store obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return _dbContext.Stores.Find(obj.Id);
        }


        private IQueryable<Store> GetStores()
        {
            return _dbContext.Stores.Select(a => a);
        }
    }
}
