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
            bool _continue = true;
            while (_continue)
            {
                Console.WriteLine("-------- Menu --------");
                Console.WriteLine("1. Generate Orders");
                Console.WriteLine("2. Simulate Fabrication");
                Console.WriteLine("3. Simulate Conditionnement");
                Console.WriteLine("4. Generate JSON");
                Console.WriteLine("5. Refresh Commandes Dates");
                Console.WriteLine("6. Drop Dynamic Data from Database");
                Console.WriteLine("7. Simulate Fabrication OPTI");
                Console.WriteLine("8. Simulate Conditionnement OPTI");
                Console.WriteLine("0. Exit");
                Console.Write("Your choice : ");
                string user_choice = Console.ReadLine();
                int choice = int.Parse(user_choice);
                if (choice >= 0 || choice <= 6)
                {
                    switch (choice)
                    {
                        case 0:
                            _continue = false;
                            break;
                        case 1:
                            //CFG_CommandesGenerator orderGenForm = new CFG_CommandesGenerator();
                            Utils.GenerateOrders();
                            break;
                        case 2:
                            //Utils.Build_UnprocessedOrders();
                            Utils.Build_UnprocessedOrders();
                            break;
                        case 3:
                            Utils.Package_UnprocessedOrders();
                            break;
                        case 4:
                            Utils.Generate_Data_001();
                            break;
                        case 5:
                            Utils.Refresh_CommandesDates();
                            break;
                        case 6:
                            Utils.Drop_AllDynamicData();
                            break;
                        case 7:
                            Utils.Build_UnprocessedOrders_Opti();
                            break;
                        case 8:
                            Utils.Package_UnprocessedOrders_Opti();
                            break;
                        case 9:
                            using (var context = new PalaisDuBonbonEntities())
                            {
                                float temps_total = 0f;
                                foreach(PERF_COMMANDES c in context.PERF_COMMANDES)
                                {
                                    int tps = (int)c.TEMPS_CONDITIONNEMENT;
                                    temps_total += tps;
                                }
                                Console.WriteLine("Moyenn fabrication : " + (temps_total / context.PERF_COMMANDES.Count()));
                            }
                                break;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a correct value...");
                }
            }
            //Utils.Initialize_BaseData(true);

            Console.WriteLine("-- END OF PROGRAM --");
        }
    }
}
