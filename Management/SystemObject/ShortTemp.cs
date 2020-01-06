using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Management
{
    public class ShortTemp
    {
        private string vcode;

        public ShortTemp(string code)
        {
            this.code = code;
        }

        public string code { get; set; }
    }
}
