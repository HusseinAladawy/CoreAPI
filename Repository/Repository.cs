using EmployeeManagement.Api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {


        private AppDbContext context = null;
        private DbSet<T> table = null;
        public Repository()
        {
            this.context = new AppDbContext();
            table = context.Set<T>();
        }
        public Repository(AppDbContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }
        public async Task Delete(object id)
        {
            var result = await GetById(id);
            if (result != null)
            {
                table.Remove(result);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var result = await table.ToListAsync();
            return result;
        }
        public async Task<IEnumerable<T>> Find(Expression<Func<T,bool>> predicate)
        {
            var result = await table.Where(predicate).ToListAsync();
            return result;
        }

        public async Task<T> GetById(object id)
        {
            var result = await table.FindAsync(id);
            return result;
        }

        public async Task<T> Insert(T entity)
        {
            var result = await table.AddAsync(entity);
           await context.SaveChangesAsync();
            return result.Entity;
        }



        public async Task<T> Update(T entity)
        {

          table.Attach(entity);
          context.Entry(entity).State = EntityState.Modified;
            
          await context.SaveChangesAsync();
          return entity;
        }
        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
