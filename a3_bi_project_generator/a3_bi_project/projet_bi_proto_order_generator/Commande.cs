using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_bi_proto_order_generator
{
    public class Commande
    {
        public Commande(int numCommande)
        {
            this.num_commande = num_commande;
        }
        public int id { get; set; }
        public int num_commande { get; set; }
        public Type_Bonbon type_bonbon { get; set; }

        public Conditionnement conditionnement { get; set; }
        public int quantite { get; set; }

        public Pays pays_destination { get; set; }

        public float prix_lots { get; set; }

        public DateTime date_commande { get; set; }

        public DateTime date_expedition { get; set; }

        public DateTime date_fabrication{ get; set; }

        public DateTime date_conditionnement{ get; set; }


        public bool etat { get; set; }
        //public int no_colis { get; set; } ??????
    }
}
