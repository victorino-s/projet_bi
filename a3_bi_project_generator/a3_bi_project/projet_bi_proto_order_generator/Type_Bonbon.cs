using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_bi_proto_order_generator
{
    public class Type_Bonbon
    {
        public Type_Bonbon(dynamic data)
        {
            this.name = data.nom;
        }
        public string name { get; set; }
    }
}
