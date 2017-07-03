using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_bi_proto_order_generator
{
    public class Transport
    {
        public Transport(dynamic data)
        {
            this.type = data.type;
            this.prix = data.prix;
        }

        public string type { get; set; }
        public float prix { get; set; }
    }
}
