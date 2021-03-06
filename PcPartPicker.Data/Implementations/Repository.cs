﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PcPartPicker.Data.Interfaces;
using PcPartPicker.Models.Models;

namespace PcPartPicker.Data.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly PcPartPickerDbContext _context;
        internal DbSet<TEntity> dbSet;

        public Repository(PcPartPickerDbContext context)
        {
            _context = context;
            dbSet = _context.Set<TEntity>();
        }

        public void Delete(object id)
        {
            TEntity entity = dbSet.Find(id);
            Delete(entity);
        }

        public void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "",
                int? skip = null, int? take = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                if (skip != null)
                {
                    query = orderBy(query).Skip((int)skip);
                }

                if (take != null)
                {
                    query = orderBy(query).Take((int)take);
                }

                return query.ToList();
            }
            else
            {
                if (skip != null)
                {
                    query = query.Skip((int)skip);
                }

                if (take != null)
                {
                    query = query.Take((int)take);
                }

                return query.ToList();
            }
        }

        public TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
        
        public void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
