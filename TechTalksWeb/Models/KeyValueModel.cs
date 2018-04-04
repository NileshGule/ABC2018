using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechTalksWeb.Models
{
    public class KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class KeyValues
    {
        public IList<KeyValue> keyValues;
    }
}
