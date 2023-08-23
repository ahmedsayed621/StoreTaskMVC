using Microsoft.EntityFrameworkCore;
using StoreTaskMVC.Data;
using StoreTaskMVC.Interfaces;
using StoreTaskMVC.Models;

namespace StoreTaskMVC.Services
{
    public class SpaceService:ISpaceService
    {
        private readonly StoreDbContext _dbContext;

        public SpaceService(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        

        public Space Delete(Space obj)
        {
            _dbContext.Spaces.Remove(obj);
            foreach (var product in _dbContext.Products)
            {
                if (obj.Id == product.SpaceId)
                {
                    _dbContext.Products.Remove(product);
                }
            }
            _dbContext.SaveChanges();
            
            return obj;

        }

        public IEnumerable<Space> Get()
        {
            return _dbContext.Spaces.ToList();
        }

        public Space GetById(int id)
        {
            var data = _dbContext.Spaces
                .Where(a => a.Id == id).SingleOrDefault();
            return data;
        }

        public Space Merge(int spaceId1, int spaceId2)
        {
            // Find the first space to merge
            var space1 = _dbContext.Spaces.Include(s => s.Products).SingleOrDefault(s => s.Id == spaceId1);

            // Find the second space to merge
            var space2 = _dbContext.Spaces.Include(s => s.Products).SingleOrDefault(s => s.Id == spaceId2);

            // Create a new space with the same Store ID
            var newSpace = new Space
            {
                Name = "Merged Space",
                StoreId = space1.StoreId,
                Products = new List<Product>()
            };

            // Move products from space1 and space2 to the new space
            foreach (var product in space1.Products)
            {
                product.SpaceId = newSpace.Id;
                newSpace.Products.Add(product);
            }
            foreach (var product in space2.Products)
            {
                product.SpaceId = newSpace.Id;
                newSpace.Products.Add(product);
            }

            // Remove the original spaces from the database
            _dbContext.Spaces.Remove(space1);
            _dbContext.Spaces.Remove(space2);
            _dbContext.SaveChanges();


            return _dbContext.Spaces.OrderBy(a => a.Id).LastOrDefault();



        }

        public Space Split(int id, int numberOfSplits)
        {
            var originalSpace = _dbContext.Spaces.SingleOrDefault(s=>s.Id==id);
            

            var newSpaces = new List<Space>();
            for (int i = 0; i < numberOfSplits; i++)
            {
                var newSpace = new Space
                {
                    Name = $"New Space {i + 1}",
                    StoreId = originalSpace.StoreId
                };
                newSpaces.Add(newSpace);
            }

            // Add the new spaces to the database
            _dbContext.Spaces.AddRange(newSpaces);
            _dbContext.SaveChanges();
            return _dbContext.Spaces.OrderBy(a => a.Id).LastOrDefault();
        }

        public Space Update(Space obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return _dbContext.Spaces.Find(obj.Id);
        }
    }
}
