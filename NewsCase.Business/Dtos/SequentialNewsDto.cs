

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NewsCase.Business.Dtos
{
    public class SequentialNewsDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int[] Rows { get; set; }
        public List<NewsDto> News { get; set; }
        public List<string> NewsIds { get; set; }
    }
}
