using Microsoft.EntityFrameworkCore;
using SpionAPI.Interfaces;
using SpionAPI_DataAccess.Data;

namespace SpionAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        public AppDbContext _context { get; }

        public GenericRepository(AppDbContext context)
        {
            this._context = context;
        }

        
        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);        //Don't need to access concrete entity name in _context class. AddAsync will automatically add class to the correct data collection/table
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();       //Set<T>() - universal method - returns DbSet of type T
        }

        public async Task<T> GetAsync(int? id)
        {
            if (id is null)
            {
                return null;
            }

            return await _context.Set<T>().FindAsync(id);       //FindAsync - najde zaznam s pozadovanym klucom/klucmi

        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}