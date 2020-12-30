using MySaleDDD.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySaleDDD.Data.Repository
{
    public interface IGenericRepository<TEntity> where TEntity:BaseEntity
    {
        IQueryable<TEntity> GetASQueryable(string includeProperties = ""); // For Joine Table Together In DataBase
        Task<IEnumerable<TEntity>> GetAllAysnc(string includeProperties = "");
        IEnumerable<TEntity> GetAll(string includeProperties = "");
        Task<TEntity> GetByIdAsync(int Id);
        Task<int> InsertAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        Task<int> DeleteAsync(int Id);
        Task<int> UpdateAsync(TEntity entity);

        int Update(TEntity entity);
        int Insert(TEntity entity);

        Task<int> InsertAllAsync(List<TEntity> entities);
        Task<int> RemoveAsync(int Id);
        Task<int> RemoveAsync(TEntity entity);
    }
}
