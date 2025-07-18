using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrearWebDDD.ModelVM
{
    public class EntityVM
    {
        public EntityVM() { }

        public bool IsSelected { get; set; }
        public string Nombre { get; set; }
        public bool IsBackOffice { get; set; }
        public bool IsJs { get; set; }
        public bool IsIdioma { get; set; }
    }
}
