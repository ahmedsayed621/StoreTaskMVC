using Microsoft.EntityFrameworkCore;
using StoreTaskMVC.Models;
using System.Reflection;

namespace StoreTaskMVC.Data
{
    public class StoreDbContext:DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options) 
        {
        
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Store>()
            //    .HasMany(s => s.Spaces)
            //    .WithOne(s => s.Store)
            //    .HasForeignKey(s => s.StoreId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Space>()
            //    .HasMany(p => p.Products)
            //    .WithOne(p => p.Space)
            //    .HasForeignKey(s => s.SpaceId)
            //    .OnDelete(DeleteBehavior.Cascade);

            

            



            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        //public override int SaveChanges()
        //{
        //    var addedStores = ChangeTracker.Entries<Store>()
        //        .Where(e => e.State == EntityState.Added)
        //        .Select(e => e.Entity);

        //    foreach (var store in addedStores)
        //    {
        //        var defaultSpace = new Space
        //        {
        //            Name = "Default Space",
        //            Store = store
        //        };
        //        Spaces.Add(defaultSpace);
        //    }


        //    return base.SaveChanges();

        //}



    }
}
