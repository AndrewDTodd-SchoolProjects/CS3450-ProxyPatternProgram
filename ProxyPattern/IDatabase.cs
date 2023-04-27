using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyPattern
{
    internal interface IDatabase
    {
        public string GetID();

        public bool Exists(string key);

        public string Get(string key);
    }
}
