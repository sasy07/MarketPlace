using System;
using System.Linq;
using System.Threading.Tasks;
using MarketPlace.DataLayer.Context;
using MarketPlace.DataLayer.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.DataLayer.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        #region constructor

        private readonly MarketPlaceDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(MarketPlaceDbContext context)
        {
            _context = context;
            this._dbSet = _context.Set<TEntity>();
        }

        #endregion

        #region CRUD

        public IQueryable<TEntity> GetQuery()
        {
            return _dbSet;
        }

        public async Task AddEntity(TEntity entity)
        {
            entity.CreateDateTime = DateTime.Now;
            entity.LastUpdateDate = entity.CreateDateTime;
            await _dbSet.AddAsync(entity);
        }

        public async Task<TEntity> GetEntityById(long entityId)
        {
            return await _dbSet.SingleOrDefaultAsync(e => e.Id == entityId);
        }

        public void EditEntity(TEntity entity)
        {
            entity.LastUpdateDate = DateTime.Now;
            _dbSet.Update(entity);
        }

        public void DeleteEntity(TEntity entity)
        {
            entity.IsDelete = true;
            EditEntity(entity);
        }

        public async Task DeleteEntity(long entityId)
        {
            var entity = await GetEntityById(entityId);
            if (entity != null)
                DeleteEntity(entity);
        }

        public void DeletePermanent(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task DeletePermanent(long entityId)
        {
            var entity = await GetEntityById(entityId);
            if (entity != null)
                DeletePermanent(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

     
        #endregion

        #region dispose

        public async ValueTask DisposeAsync()
        {
            if (_context != null)
                await _context.DisposeAsync();
        }


        #endregion
    }
}