using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrearWebDDD.CrossCutting.Class
{
    public class Property
    {
        public string ClassName { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
        public bool IsRequired { get; set; }
        public CustomAttributes CustomAttributes { get; set; }

    }
    public class CustomAttributes
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int MaxValue { get; set; }
        public int MinValue { get; set; }
    }
}
