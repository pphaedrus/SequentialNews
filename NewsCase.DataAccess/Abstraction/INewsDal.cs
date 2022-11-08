
using NewsCase.DataAccess.Abstraction.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsCase.DataAccess.Abstraction
{
    public interface INewsDal<T,C,U>: IRepository<T,C,U>
    {
        Task<bool> CreateManyAsync(IEnumerable<C> entity);
    }
}
