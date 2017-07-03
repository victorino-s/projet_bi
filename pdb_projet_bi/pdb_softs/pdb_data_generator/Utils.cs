using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace pdb_data_generator
{
    public static class Utils
    {
        #region InitData
        public static string GetFactoryData()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"_static_files", "factory_data.json");
            return File.ReadAllText(path);
        }

        public static dynamic GetFactoryDataJSON()
        {
            return JsonConvert.DeserializeObject(GetFactoryData());
        }

        public static void Initialize_VariantesData()
        {
            var data = GetFactoryDataJSON().variantes;

            using (var context = new PalaisDuBonbonEntities())
            {
                Console.WriteLine("---- Initialize Variantes Table : Data To Procces : " + data.Count);
                int c = 1;
                foreach (var v in data)
                {
                    VARIANTE newvar = new VARIANTE();
                    newvar.VARIANTE_NOM = v;
                    context.VARIANTEs.Add(newvar);
                    Console.WriteLine("-------- Data Added : (" + c + "/" + data.Count + ")");
                    c++;
                }
                context.SaveChanges();
                Console.WriteLine("---- Initialize Variantes Table : DONE ! ----");
            }
        }

        public static void Initialize_ConditionnementData()
        {
            var data = GetFactoryDataJSON().conditionnements;

            using (var context = new PalaisDuBonbonEntities())
            {
                Console.WriteLine("---- Initialize Conditionnement Table : Data To Procces : " + data.Count);
                int c = 1;
                foreach (var d in data)
                {
                    CONDITIONNEMENT newvar = new CONDITIONNEMENT();
                    newvar.CONDITIONNEMENT_NOM = d.nom;
                    newvar.CONDITIONNEMENT_QUANTITE_MAX = d.quantite;
                    context.CONDITIONNEMENTs.Add(newvar);
                    Console.WriteLine("-------- Data Added : (" + c + "/" + data.Count + ")");
                    c++;
                }
                context.SaveChanges();
                Console.WriteLine("---- Initialize Conditionnement Table : DONE ! ----");
            }
        }

        public static void Initialize_TransportInfoData()
        {
            var data = GetFactoryDataJSON().transports;

            using (var context = new PalaisDuBonbonEntities())
            {

                Console.WriteLine("---- Initialize TransportInfo Table : Data To Procces : " + data.Count);
                int c = 1;
                foreach (var d in data)
                {
                    TRANSPORT_INFO ti = new TRANSPORT_INFO();
                    ti.TRANSPORT_INFO_TYPE_TRANSPORT = d.nom;
                    ti.TRANSPORT_INFO_QUANTITE_PALETTE = d.qte_palette;
                    ti.TRANSPORT_INFO_CARTON_PAR_PALETTE = d.qte_carton_palette;
                    context.TRANSPORT_INFO.Add(ti);
                    Console.WriteLine("-------- Data Added : (" + c + "/" + data.Count + ")");
                    c++;
                }
                context.SaveChanges();
                Console.WriteLine("---- Initialize TransportInfo Table : DONE ! ----");
            }
        }

        /// <summary>
        /// This Function MUST be executed AFTER having populated the Transport Table.
        /// 
        /// TO DO : Secure the function with checking on opening the connexion if Transport Table already exists,
        /// if not, execute the Initialize_TransportInfo Method.
        /// </summary>
        public static void Initialize_PaysData()
        {
            var data = GetFactoryDataJSON().pays;

            using (var context = new PalaisDuBonbonEntities())
            {
                Console.WriteLine("---- Initialize Pays Table : Data To Procces : " + data.Count);
                int c = 1;

                List<string> transports_avion = new List<string>();
                transports_avion.Add("USA");
                transports_avion.Add("Canada");
                transports_avion.Add("Mexique");
                transports_avion.Add("Chine");

                List<string> transports_bateau = new List<string>();
                transports_bateau.Add("Japon");
                transports_bateau.Add("Afrique du Sud");


                foreach (var d in data)
                {
                    PAY p= new PAY();
                    p.PAYS_NOM = d.Value;

                    if(transports_bateau.Contains(d.Value))
                    {
                        p.PAYS_TRANSPORT = "bateau";
                    }
                    else if(transports_avion.Contains(d.Value))
                    {
                        p.PAYS_TRANSPORT = "avion";
                    }
                    else
                    {
                        p.PAYS_TRANSPORT = "camion";
                    }
                    
                    context.PAYS.Add(p);
                    Console.WriteLine("-------- Data Added : (" + c + "/" + data.Count + ")");
                    c++;
                   
                }

                context.SaveChanges();
                Console.WriteLine("---- Initialize Pays Table : DONE ! ----");
            }
        }

        /// <summary>
        /// This Function MUST be executed AFTER having populated the Variante Table.
        /// 
        /// TO DO : Secure the function with checking on opening the connexion if Variante Table already exists,
        /// if not, execute the Initialize_VarianteData Method.
        /// </summary>
        public static void Initialize_BonbonData()
        {
            var data = GetFactoryDataJSON();
            var data_types_bonbons = data.types_bonbon;
            var data_textures = data.textures;
            var data_couleurs = data.couleurs_bonbons;
            var data_variantes = data.variantes;

            int data_count = data_types_bonbons.Count * data_textures.Count * data_couleurs.Count * data_variantes.Count;
            using (var context = new PalaisDuBonbonEntities())
            {

                Console.WriteLine("---- Initialize Bonbon Table : Data To Procces : " + data.Count);
                int c = 1;
                foreach (var type_bonbon in data_types_bonbons)
                {
                    foreach(var texture in data_textures)
                    {
                        foreach(var couleur in data_couleurs)
                        {
                            foreach(var variante in data_variantes)
                            {
                                string _variante = variante;
                                BONBON bonbon = new BONBON();

                                bonbon.BONBON_TYPE = type_bonbon;
                                bonbon.BONBON_TEXTURE = texture;
                                bonbon.BONBON_COULEUR = couleur;
                                bonbon.VARIANTE = context.VARIANTEs.FirstOrDefault(v => v.VARIANTE_NOM == _variante);
                                bonbon.VARIANTE_ID = context.VARIANTEs.FirstOrDefault(v => v.VARIANTE_NOM == bonbon.VARIANTE.VARIANTE_NOM).VARIANTE_ID;

                                if (bonbon.VARIANTE == null)
                                    Console.WriteLine("ERROR ! bonbon.VARIANTE  Object is null !");

                                context.BONBONs.Add(bonbon);
                                Console.WriteLine("-------- Data Added : (" + c + "/" + data_count + ")");
                                c++;
                                if(c % 100 == 0)
                                {
                                    Console.WriteLine("Pushing to Database...");
                                    context.SaveChanges();
                                }
                            }
                        }
                    }
                }
                context.SaveChanges();
                Console.WriteLine("---- Initialize Bonbon Table : DONE ! ----");
            }
        }

        #endregion
    }
}
