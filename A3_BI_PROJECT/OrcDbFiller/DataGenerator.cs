using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace OrcDbFiller
{
    public static class DataGenerator
    {
        public static void Generate_CandyData(out dynamic candyList)
        {
            List<string> _styles = new List<string>();
            using(StreamReader sr = new StreamReader("../../_local_data/styles_bonbons.json"))
            {
                string json = sr.ReadToEnd();
                JsonFileContentObj obj = JsonConvert.DeserializeObject<JsonFileContentObj>(json);

                foreach(string s in obj.content)
                {
                    Console.WriteLine(s);
                }
            }
            candyList = _styles;
            Console.WriteLine("Done");
        }

        public static void Generate_OrderData()
        {

        }
    }
}
