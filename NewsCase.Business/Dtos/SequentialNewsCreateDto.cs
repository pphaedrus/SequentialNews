
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewsCase.Business.Dtos
{
    public class SequentialNewsCreateDto
    {
        [Required]
        public string Title { get; set; }
        public int[] Rows { get; set; }
        public List<string> NewsIds { get; set; }
    }
}
