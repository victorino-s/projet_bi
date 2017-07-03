using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_bi_proto_order_generator
{
    public class Pays
    {
        public string name { get; set; }
        public Pays(dynamic data)
        {
            this.name = data.nom;
            this.transport = data.transport;
        }

        public Transport transport { get; set; }
    }
}
