using MySaleDDD.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MySaleDDD.Data.Repository
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAllAysnc();
        IEnumerable<Brand> GetAll();
        Task<Brand> GetByIdAsync(int Id);
        Task<int> InsertAsync(Brand entity);
        Task<int> DeleteAsync(Brand entity);
        Task<int> DeleteAsync(int Id);
        Task<int> UpdateAsync(Brand entity);

        int Update(Brand entity);
        int Insert(Brand entity);

        Task<int> InsertAllAsync(List<Brand> entities);
        Task<int> RemoveAsync(int Id);
        Task<int> RemoveAsync(Brand entity);
    }
}
