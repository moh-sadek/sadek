﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Managegment.objects
{
    public class CustomersObj
    {
        public long custmorId { get; set; }
        public long? UnknownNumberId { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public DateTime date { get; set; }
        public string email { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string serviceName { get; set; }
        public string code { get; set; }
        public int amount { get; set; }
        public int countMassage { get; set; }
        public int countUseMassage { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public string discriptions { get; set; }


    }

}