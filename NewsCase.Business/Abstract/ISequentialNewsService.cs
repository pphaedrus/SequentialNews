

using NewsCase.Business.Dtos;
using NewsCase.Core.Common;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewsCase.DataAccess.Abstraction;
namespace NewsCase.Business.Abstract
{
    public interface ISequentialNewsService: ISequentialNewsDal<SequentialNewsDto, SequentialNewsCreateDto, SequentialNewsUpdateDto, NewsDto>
    {
    }
}
