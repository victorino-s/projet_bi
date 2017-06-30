using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcDbFiller
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic test;
            DataGenerator.Generate_CandyData(out test);
        }
    }
}
