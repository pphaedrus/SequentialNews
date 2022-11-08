
using NewsCase.Business.Dtos;
using NewsCase.Core.Common;
using NewsCase.DataAccess.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsCase.Business.Abstract
{
    public interface INewsService:INewsDal<NewsDto, NewsCreateDto, NewsUpdateDto>
    {

    }
}
