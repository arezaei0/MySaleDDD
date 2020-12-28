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
    public class BrandRepository : IBrandRepository
    {
        private readonly DataContext _context;
        public DbSet<Brand> dbset;
        string errorMessage;

        public BrandRepository(DataContext context)
        {
            _context = context;
            this.dbset = context.Set<Brand>();
        }

        public async Task<int> DeleteAsync(Brand entity)
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

        public  IEnumerable<Brand> GetAll()
        {
            return dbset.Where(w => !w.IsDeleted).ToList();
        }

        public async Task<IEnumerable<Brand>> GetAllAysnc()
        {
            return await dbset.Where(w => !w.IsDeleted).ToListAsync();
        }

        public async Task<Brand> GetByIdAsync(int Id)
        {
            return await dbset.FindAsync(Id);
            
        }

        public int Insert(Brand entity)
        {
            try
            {
                entity.AddedDate = DateTime.Now;
                dbset.Add(entity);
                return  _context.SaveChanges();
            }
            catch
            {

                return -1;
            }
        }

        public async Task<int> InsertAllAsync(List<Brand> entities)
        {
            dbset.AddRange(entities);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> InsertAsync(Brand entity)
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
            if(_context.Entry(entity).State== EntityState.Detached)
            {
                dbset.Attach(entity);
            }
            dbset.Remove(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> RemoveAsync(Brand entity)
        {
            dbset.Remove(entity);
            return await _context.SaveChangesAsync();
        }
        public int Update(Brand entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(Brand entity)
        {
            throw new NotImplementedException();
        }
    }
}
