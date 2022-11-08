using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsCase.DataAccess.Abstraction.Repository
{
    public interface IRepository<T, C, U>
    {
        Task<T> GetByIdAsync(string id);
        Task<bool> UpdateAsync(U updateDto);
        Task<bool> DeleteAsync(string id);
        Task<List<T>> GetAllAsync();
    }
}
