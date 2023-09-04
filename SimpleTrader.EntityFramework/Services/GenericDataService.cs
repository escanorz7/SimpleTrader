using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.EntityFramework.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        private readonly SimpleTraderDbContext _context;

        public GenericDataService(SimpleTraderDbContext context)
        {
            _context = context;
        }

        public async Task<T> Create(T entity)
        {
            var createdResult = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return createdResult.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            T entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<T> Get(int id)
        {
            T entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            IEnumerable<T> entities = await _context.Set<T>().ToListAsync();
            return entities;
        }

        public async Task<T> Update(int id, T entity)
        {
            entity.Id = id;
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
