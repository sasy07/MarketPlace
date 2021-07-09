﻿using System;
using System.Linq;
using System.Threading.Tasks;
using MarketPlace.DataLayer.Entities.Common;

namespace MarketPlace.DataLayer.Repository
{
    public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity:BaseEntity
    {
        IQueryable<TEntity> GetQuery();
        Task AddEntity(TEntity entity);
        Task<TEntity> GetEntityById(long entityId);
        void EditEntity(TEntity entity);
        void DeleteEntity(TEntity entity);
        Task DeleteEntity(long entityId);
        void DeletePermanent(TEntity entity);
        Task DeletePermanent(long entityId);
        Task SaveChanges();
    }
}