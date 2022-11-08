using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsCase.Business.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string NewsCollectionName { get; set; }
        public string SequentialNewsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
