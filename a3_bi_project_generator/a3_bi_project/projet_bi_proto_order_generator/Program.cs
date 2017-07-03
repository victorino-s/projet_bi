using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_bi_proto_order_generator
{
    class Program
    {
        static string output_path = Directory.GetCurrentDirectory() + "../../../_local_data/output.json";
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            
            string configText = File.ReadAllText(Directory.GetCurrentDirectory() + "../../../_local_data/config.json");
            dynamic jsonConfigFile = JsonConvert.DeserializeObject(configText);

            List<Variante> variantes_config = new List<Variante>();
            List<Type_Bonbon> types_bonbon_config = new List<Type_Bonbon>();
            List<Conditionnement> conditionnements_config = new List<Conditionnement>();
            List<Pays> pays_config = new List<Pays>();

            List<Commande> _generated_order = new List<Commande>();


            int lots_min = jsonConfigFile.lots_min;
            int lots_max = jsonConfigFile.lots_max;
            int nb_commandes = jsonConfigFile.nb_commandes;

            foreach (var v in jsonConfigFile.variantes)
            {
                variantes_config.Add(new Variante(v));
            }
            foreach(var tb in jsonConfigFile.types_bonbon)
            {
                types_bonbon_config.Add(new Type_Bonbon(tb));
            }
            foreach(var c in jsonConfigFile.conditionnements)
            {
                conditionnements_config.Add(new Conditionnement(c));
            }
            foreach(var p in jsonConfigFile.pays)
            {
                pays_config.Add(new Pays(p));
            }

            for(int i = 0; i < nb_commandes; i++)
            {
                int nb_lots = rnd.Next(lots_min, lots_max);
                int randomPays = rnd.Next(0, pays_config.Count);

                for (int n = 0; n < nb_lots; n++)
                {
                    Commande _commande = new Commande(i);
                    
                    int randomVariante = rnd.Next(0, variantes_config.Count);
                    int randomConditionnement = rnd.Next(0, conditionnements_config.Count);
                    int randomType = rnd.Next(0, types_bonbon_config.Count);
                    int quantite = rnd.Next(1, 2000);

                    _commande.id = i*10+n; // TEMP ID
                    _commande.type_bonbon = types_bonbon_config[randomType];
                    _commande.type_bonbon.variante = variantes_config[randomVariante];
                    _commande.conditionnement = conditionnements_config[randomConditionnement];
                    _commande.quantite = quantite;
                    _commande.pays_destination = pays_config[randomPays];
                    _commande.prix_lots = 0f;

                    _generated_order.Add(_commande);
                }
            }

            File.WriteAllText(output_path, JsonConvert.SerializeObject(_generated_order));
            Console.WriteLine(_generated_order.Count + " Commandes générées..");


            Console.ReadLine();
        }
    }
}
