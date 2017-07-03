using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_bi_proto_order_generator
{
    public class Variante
    {
        public Variante(dynamic data)
        {
            this.name = data.nom;
            this.machines_compatibles = data.machines_compatible;
        }
        public string name { get; set; }
        public dynamic machines_compatibles { get; set; }
    }
}
