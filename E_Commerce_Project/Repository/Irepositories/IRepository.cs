﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace E_Commerce_Project.Repository.Irepositories
{
    public interface IRepository<T> where T : class
    {
        public void Create(T entity);

        public void Create(List<T> entity);


        public void Edit(T entity);

        public void Delete(T entity);

        public void Delete(List<T> entity);

        public void comit();

        public IEnumerable<T> Get(
     Expression<Func<T, bool>>? filter = null,
     Expression<Func<T, object>>[]? includes = null,
     bool tracked = true);
       

        public T? GetOne(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>[]? includes = null, bool tracked = true)
        {
            return Get(filter, includes, tracked).FirstOrDefault();
        }

    }
}

