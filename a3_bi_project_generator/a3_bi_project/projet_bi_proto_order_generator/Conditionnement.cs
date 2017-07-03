using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_bi_proto_order_generator
{
    public class Conditionnement
    {
        public Conditionnement(dynamic data)
        {
            this.name = data.nom;
            this.machines_compatibles = data.machines_compatibles;
            this.couts_conditionnement = data.couts_conditionnement;
            this.quantite_bonbons = data.qte_bonbon;
        }
        public string name { get; set; }
        public string machines_compatibles { get; set; }

        public float couts_conditionnement{ get; set; }

        public int quantite_bonbons { get; set; }
    }
}
