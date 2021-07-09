using System.Linq;
using MarketPlace.DataLayer.Entities.Account;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.DataLayer.Context
{
    public class MarketPlaceDbContext:DbContext
    {
        #region constructor

        public MarketPlaceDbContext(DbContextOptions<MarketPlaceDbContext> options):base(options)
        {
            
        }

        #endregion
        
        #region Account

        public DbSet<User> Users { get; set; }

        #endregion

        #region on model creating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e=>e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
            }
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}