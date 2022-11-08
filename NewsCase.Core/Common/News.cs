using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsCase.Core.Entities;

namespace NewsCase.Core.Common
{
    public class News: BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime ChangedDate { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public int QueueNumber { get; set; }
    }
}
