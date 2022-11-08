using System;

namespace NewsCase.Business.Dtos
{
    public class NewsCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}
