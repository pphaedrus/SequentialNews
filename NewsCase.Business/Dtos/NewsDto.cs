
using System;

namespace NewsCase.Business.Dtos
{
    public class NewsDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime ChangedDate { get; set; }
        public int QueueNumber { get; set; }
    }
}
