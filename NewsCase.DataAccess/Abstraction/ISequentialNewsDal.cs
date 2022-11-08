
using NewsCase.DataAccess.Abstraction.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsCase.DataAccess.Abstraction
{
    public interface ISequentialNewsDal<T,C,U,K>: IRepository<T,C,U>
    {
        Task<bool> CreateAsync(C entity);
        Task<List<K>> GetByTitleAsync(string title);
    }
}
