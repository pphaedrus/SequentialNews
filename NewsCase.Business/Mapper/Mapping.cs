using AutoMapper;
using NewsCase.Business.Dtos;
using NewsCase.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace NewsCase.Business.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<News, NewsCreateDto>().ReverseMap();
            CreateMap<News, NewsUpdateDto>().ReverseMap();
            CreateMap<News, NewsDto>().ReverseMap();
            
            CreateMap<SequentialNews, SequentialNewsCreateDto>().ReverseMap();
            CreateMap<SequentialNews, SequentialNewsUpdateDto>().ReverseMap();
            CreateMap<SequentialNews, SequentialNewsDto>().ReverseMap();

            CreateMap<SequentialNews, SequentialNewsDto>();
        }

    }
}
