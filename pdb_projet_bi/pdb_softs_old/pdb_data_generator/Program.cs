using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdb_data_generator
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Utils.Initialize_VariantesData();
            Utils.Initialize_ConditionnementData();
            Utils.Initialize_TransportInfoData(); 
            */
            //Utils.Initialize_PaysData();
            //Utils.Initialize_BonbonData();
            //Utils.Initialize_Pays_RatioCommandes();
            //Utils.Initialize_CartonInfos();
            //Utils.Initialize_Recettes();
            //Utils.Initialize_PrixLots();

            Utils.Initialize_MachinesConditionnement();
            Console.ReadLine();
        }
    }
}
