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
            //Utils.Initialize_BaseData(true);
            Console.WriteLine("-------- Menu --------");
            Console.WriteLine("1. Generate Orders");

            string user_choice = Console.ReadLine();

            switch(user_choice)
            {
                case "1":
                    //CFG_CommandesGenerator orderGenForm = new CFG_CommandesGenerator();
                    Utils.GenerateOrders();
                    break;
            }
            Console.WriteLine("-- END OF PROGRAM --");
        }
    }
}
