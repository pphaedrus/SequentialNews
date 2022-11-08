using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NewsCase.Core.Entities;

namespace NewsCase.Core.Common
{
    public class SequentialNews:BaseEntity
    {
        public string Title { get; set; }
        public int[] Rows { get; set; }//haber sırası tutar
        [JsonIgnore]
        public List<News> News { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> NewsIds { get; set; }
    }
}
