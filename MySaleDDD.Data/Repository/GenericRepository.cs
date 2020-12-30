using Microsoft.EntityFrameworkCore;
using MySaleDDD.Core;
using MySaleDDD.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaleDDD.Data.Repository
{
    public class GenericRepository<Tentity> : IGenericRepository<Tentity> where Tentity : BaseEntity
    {
        private readonly DataContext _context;
        public DbSet<Tentity> dbset;
        string errorMessage;

        public GenericRepository(DataContext context)
        {
            _context = context;
            this.dbset = context.Set<Tentity>();
        }
        public IQueryable<Tentity> GetASQueryable(string includeProperties = "")
        {
            IQueryable<Tentity> query = dbset;
            query = query.Where(x => !x.IsDeleted);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }

            }
            return query;
        }

        public async Task<int> DeleteAsync(Tentity entity)
        {
            entity.IsDeleted = true;
            entity.DeleteDate = DateTime.Now;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int Id)
        {
            var entity = await dbset.FindAsync(Id);
            entity.IsDeleted = true;
            entity.DeleteDate = DateTime.Now;
            return await _context.SaveChangesAsync();
        }

        public IEnumerable<Tentity> GetAll(string includeProperties = "")
        {
            if (!string.IsNullOrEmpty(includeProperties))
            {
                return dbset.Where(x => !x.IsDeleted).Include(includeProperties).ToList(); // We Don't Want Multi Join. We want only One Join. If We want Multi Join we should return Iqueryable Type.
            }
            else
            {
                return dbset.Where(x => !x.IsDeleted).ToList();
            }
        }

        public async Task<IEnumerable<Tentity>> GetAllAysnc(string includeProperties = "")
        {
            if (!string.IsNullOrEmpty(includeProperties))
            {
                return await dbset.Where(x => !x.IsDeleted).Include(includeProperties).ToListAsync(); 
            }
            else
            {
                return await dbset.Where(x => !x.IsDeleted).ToListAsync();
            }
        }

        public async Task<Tentity> GetByIdAsync(int Id)
        {
            return await dbset.FindAsync(Id);

        }

        public int Insert(Tentity entity)
        {
            try
            {
                entity.AddedDate = DateTime.Now;
                dbset.Add(entity);
                return _context.SaveChanges();
            }
            catch
            {

                return -1;
            }
        }

        public async Task<int> InsertAllAsync(List<Tentity> entities)
        {
            dbset.AddRange(entities);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> InsertAsync(Tentity entity)
        {
            try
            {
                entity.AddedDate = DateTime.Now;
                dbset.Add(entity);
                return await _context.SaveChangesAsync();
            }
            catch
            {

                return -1;
            }
        }

        public async Task<int> RemoveAsync(int Id)
        {
            var entity = await dbset.FindAsync(Id);
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbset.Attach(entity);
            }
            dbset.Remove(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> RemoveAsync(Tentity entity)
        {
            dbset.Remove(entity);
            return await _context.SaveChangesAsync();
        }
        public int Update(Tentity entity)
        {
            entity.LastModified = DateTime.Now;
            _context.Update(entity);
            return _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(Tentity entity)
        {
            entity.LastModified = DateTime.Now;
            _context.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
