using Microsoft.EntityFrameworkCore;
using MyLittleEquipmentTrader.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyLittleEquipmentTrader.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        // Private readonly context to interact with the database
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        // Constructor to inject the DbContext
        public Repository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); // Add null check for context
            _dbSet = context.Set<T>(); // Set the DbSet for the generic type
        }

        // Get IQueryable for dynamic queries (no includes)
        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable(); // Return IQueryable of T
        }

        // Get IQueryable with optional includes (new method)
        public IQueryable<T> GetQueryableWithIncludes(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.AsQueryable();

            // Include the properties specified in the includes parameter
            if (includes != null && includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include); // Add each included property to the query
                }
            }

            return query; // Return the query with includes
        }

        // Get all entities asynchronously
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync(); // Return all entities as a list asynchronously
        }

        // Optional: Method to save changes to the context
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync(); // Commit the changes to the database asynchronously
        }

        // Optional: Method to handle adding an entity
        public async Task AddAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity); // Add the entity asynchronously
            await SaveChangesAsync(); // Save changes to the database
        }

        // Optional: Method to handle updating an entity
        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Update(entity); // Update the entity in the DbSet
            await SaveChangesAsync(); // Save changes to the database
        }

        // Optional: Method to handle deleting an entity by ID
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id); // Find the entity by ID
            if (entity != null)
            {
                _dbSet.Remove(entity); // Remove the entity
                await SaveChangesAsync(); // Save changes to the database
            }
        }

        // Optional: Method to get an entity by ID
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id); // Find and return the entity by ID asynchronously
        }
    }
}
