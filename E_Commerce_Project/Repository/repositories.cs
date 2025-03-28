﻿using E_Commerce_Project.DataAcess;
using E_Commerce_Project.Models;
using E_Commerce_Project.Repository.Irepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_Commerce_Project.Repository
{
    //crud
    public class repositories<T>: IRepository<T> where T : class
    {
        //ApplicationDbContext dbContext=new ApplicationDbContext();
         //ApplicationDbContext dbContext;
        private readonly ApplicationDbContext dbContext;
        public DbSet<T> dbset;



        public repositories(ApplicationDbContext applicationDb) {
            this.dbContext = applicationDb;
            dbset = dbContext.Set<T>();
        }
        public void Create(T entity) {
            dbset.Add(entity);
            dbContext.SaveChanges();
        }
        public void Create(List<T> entity) {
            dbset.AddRange(entity);
            dbContext.SaveChanges();
        }

        public void Edit(T entity)
        {
            dbset.Update(entity);
            dbContext.SaveChanges();
        }
        public void Delete(T entity)
        {
            dbset.Remove(entity);
            dbContext.SaveChanges();
        }
        public void Delete(List<T> entity)
        {
            dbset.RemoveRange(entity);
            dbContext.SaveChanges();
        }
        public void comit() {
            dbContext.SaveChanges();
        }
        public IEnumerable<T> Get(
     Expression<Func<T, bool>>? filter = null,
     Expression<Func<T, object>>[]? includes = null,
     bool tracked = true)
        {
            IQueryable<T> query = dbset;

            // Apply filter only if it's not null
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Include navigation properties if provided
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            // Disable tracking if specified
            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            return query.ToList();
        }

        public T? GetOne(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includes = null, bool tracked = true)
        {
            return Get(filter, includes, tracked).FirstOrDefault();
        }

    }
}
