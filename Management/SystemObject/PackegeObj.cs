using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Management.objects
{
    public class PackegeObj
    {
    public long custmorId { get; set; }
    public long? UnknownNumberId { get; set; }
        public string serviceName { get; set; }
        public string code { get; set; }
        public int amount { get; set; }
        public int countMassage { get; set; }
        public int countUseMassage { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }

    }
}
