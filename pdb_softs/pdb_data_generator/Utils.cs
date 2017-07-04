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
        #region UtilsFields
        public static Random rnd = new Random();
        #endregion

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

        public static string GetConfigOrderGenerationData()
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"_static_files", "config_order_generation.json");
            return File.ReadAllText(path);
        }

        public static void GenerateOrdersJSONFile(string json)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"_static_files", "generated_orders.json");
            File.WriteAllText(path, json);
            Console.WriteLine("---- *** DATA GENERATED ** New generated_orders.json created at : " + path);
        }

        public static dynamic GetConfigOrderGenerationDataJSON()
        {
            return JsonConvert.DeserializeObject(GetConfigOrderGenerationData());
        }

        static void Initialize_VariantesData()
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

        static void Initialize_ConditionnementData()
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

        static void Initialize_TransportInfoData()
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
        static void Initialize_PaysData()
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
                    PAYS p = new PAYS();
                    p.PAYS_NOM = d.Value;

                    if (transports_bateau.Contains(d.Value))
                    {
                        p.PAYS_TRANSPORT = "bateau";
                    }
                    else if (transports_avion.Contains(d.Value))
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
        static void Initialize_BonbonData()
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
                    foreach (var texture in data_textures)
                    {
                        foreach (var couleur in data_couleurs)
                        {
                            foreach (var variante in data_variantes)
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
                                if (c % 100 == 0)
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

        static void Initialize_Pays_RatioCommandes()
        {
            var data = GetFactoryDataJSON().ratio_commandes;

            using (var context = new PalaisDuBonbonEntities())
            {

                Console.WriteLine("---- Initialize Pays Table (RatioCommandes) : Data To Procces : " + data.Count);
                int c = 1;

                int i = 0;
                foreach (PAYS p in context.PAYS)
                {
                    p.RATIO_COMMANDE = data[i];
                    i++;

                    Console.WriteLine("-------- Data Added : (" + c + "/" + data.Count + ")");
                    c++;
                }

                Console.WriteLine("---- Initialize Pays Table (RatioCommandes) : DONE ! ----");
                context.SaveChanges();
            }
        }

        static void Initialize_CartonInfos()
        {
            var data = GetFactoryDataJSON().carton_infos;

            using (var context = new PalaisDuBonbonEntities())
            {
                Console.WriteLine("---- Initialize Carton_Info Table : Data To Procces : " + data.Count);
                int c = 1;

                foreach (var d in data)
                {
                    CARTON_INFO ci = new CARTON_INFO();
                    ci.CARTON_INFO_TYPE_CONDITIONNEMENT = d.type;
                    ci.CARTON_INFO_QUANTITE = d.quantite;
                    context.CARTON_INFO.Add(ci);
                    Console.WriteLine("-------- Data Added : (" + c + "/" + data.Count + ")");
                    c++;
                }

                Console.WriteLine("---- Initialize Carton_Info Table : DONE ! ----");
                context.SaveChanges();
            }
        }

        static void Initialize_Recettes()
        {
            var data = GetFactoryDataJSON().recettes;

            using (var context = new PalaisDuBonbonEntities())
            {
                Console.WriteLine("---- Initialize Recettes Table : Data To Procces : " + data.Count);
                int c = 1;

                foreach(var d in data)
                {
                    RECETTE r = new RECETTE();
                    r.RECETTE_TYPE = d.type;
                    r.RECETTE_ADDITIF = d.additifs;
                    r.RECETTE_ENROBAGE = d.enrobage;
                    r.RECETTE_AROME = d.arome;
                    r.RECETTE_GELIFIANT = d.gelifiant;
                    r.RECETTE_SUCRE = d.sucre;

                    context.RECETTEs.Add(r);
                    Console.WriteLine("-------- Data Added : (" + c + "/" + data.Count + ")");
                    c++;
                }
                Console.WriteLine("---- Initialize Recettes Table : DONE ! ----");
                context.SaveChanges();
            }
        }

        static void Initialize_PrixLots()
        {
            var data = GetFactoryDataJSON().prix_lots_info;

            using (var context = new PalaisDuBonbonEntities())
            {
                Console.WriteLine("---- Initialize Prix Lots Table : Data To Procces : " + data.Count * 3);
                int c = 1;

                foreach (var d in data)
                {
                    string lot_type = d.type;

                    PRIX_LOT pl_sachet = new PRIX_LOT();
                    pl_sachet.BONBON = context.BONBONs.FirstOrDefault(b => b.BONBON_TYPE == lot_type);
                    pl_sachet.CONDITIONNEMENT = context.CONDITIONNEMENTs.FirstOrDefault(cond => cond.CONDITIONNEMENT_NOM == "sachet");
                    pl_sachet.BONBON_ID = pl_sachet.BONBON.BONBON_ID;
                    pl_sachet.CONDITIONNEMENT_ID = pl_sachet.CONDITIONNEMENT.CONDITIONNEMENT_ID;
                    pl_sachet.PRIX_LOT_PRIX = d.sachet;
                    context.PRIX_LOT.Add(pl_sachet);
                    c++;
                    Console.WriteLine("-------- Data Added : (" + c + "/" + data.Count + ")");

                    PRIX_LOT pl_boite = new PRIX_LOT();
                    pl_boite.BONBON = context.BONBONs.FirstOrDefault(b => b.BONBON_TYPE == lot_type);
                    pl_boite.CONDITIONNEMENT = context.CONDITIONNEMENTs.FirstOrDefault(cond => cond.CONDITIONNEMENT_NOM == "boite");
                    pl_boite.BONBON_ID = pl_boite.BONBON.BONBON_ID;
                    pl_boite.CONDITIONNEMENT_ID = pl_boite.CONDITIONNEMENT.CONDITIONNEMENT_ID;
                    pl_boite.PRIX_LOT_PRIX = d.boite;
                    context.PRIX_LOT.Add(pl_boite);
                    c++;
                    Console.WriteLine("-------- Data Added : (" + c + "/" + data.Count + ")");

                    PRIX_LOT pl_echantillon = new PRIX_LOT();
                    pl_echantillon.BONBON = context.BONBONs.FirstOrDefault(b => b.BONBON_TYPE == lot_type);
                    pl_echantillon.CONDITIONNEMENT = context.CONDITIONNEMENTs.FirstOrDefault(cond => cond.CONDITIONNEMENT_NOM == "echantillon");
                    pl_echantillon.BONBON_ID = pl_echantillon.BONBON.BONBON_ID;
                    pl_echantillon.CONDITIONNEMENT_ID = pl_echantillon.CONDITIONNEMENT.CONDITIONNEMENT_ID;
                    pl_echantillon.PRIX_LOT_PRIX = d.echantillon;
                    context.PRIX_LOT.Add(pl_echantillon);
                    c++;
                    Console.WriteLine("-------- Data Added : (" + c + "/" + (data.Count * 3) + ")");

                    if((c%100) == 0)
                    {
                        context.SaveChanges();
                    }
                }
                context.SaveChanges();
                Console.WriteLine("---- Initialize Prix Lots Table : DONE ! ----");
            }
        }

        static void Initialize_MachinesFabrication()
        {
            // Nope ! Table is wrong
        }

        static void Initialize_MachinesConditionnement()
        {
            var data = GetFactoryDataJSON().machines_conditionnement;

            using (var context = new PalaisDuBonbonEntities())
            {
                Console.WriteLine("---- Initialize MACHINE_CONDITIONNEMENT Table : Data To Procces : " + data.Count);
                int data_counter = 1;
                foreach(var d in data)
                {
                    MACHINE_CONDITIONNEMENT mc = new MACHINE_CONDITIONNEMENT();
                    mc.MACHINE_CONDITIONNEMENT_CADENCE = d.cadence;
                    mc.MACHINE_CONDITIONNEMENT_CHANGEMENT_OUTIL = d.changement_outil;

                    context.MACHINE_CONDITIONNEMENT.Add(mc);

                    Console.WriteLine("-------- Data Added : (" + data_counter + "/" + data.Count + ")");
                    data_counter++;
                }
                context.SaveChanges();
                Console.WriteLine("---- Initialize MACHINE_CONDITIONNEMENT Table : DONE ! ----");
            }
        }

        public static void Initialize_TauxVentes()
        {
            var data = GetFactoryDataJSON().taux_ventes_2016;

            using (var context = new PalaisDuBonbonEntities())
            {
                Console.WriteLine("---- Initialize TAUX_VENTES Table : Data To Procces : " + data.Count);
                int data_counter = 1;
                foreach (var d in data)
                {
                    TAUX_VENTES tv = new TAUX_VENTES();
                    //tv.TAUX_VENTE_ID = data_counter;
                    tv.TAUX_VENTE_ANNEE = 2016;
                    tv.TAUX_VENTE_BONBON = d.type_bonbon;
                    tv.TAUX_VENTE_VALEUR = d.valeur;

                    context.TAUX_VENTES.Add(tv);

                    Console.WriteLine("-------- Data Added : (" + data_counter + "/" + data.Count + ")");
                    data_counter++;
                }

                context.SaveChanges();
                Console.WriteLine("---- Initialize TAUX_VENTES Table : DONE ! ----");
            }
        }

        public static void Initialize_Couts()
        {
            var data = GetFactoryDataJSON().couts_bonbon;

            using (var context = new PalaisDuBonbonEntities())
            {
                Console.WriteLine("---- Initialize COUTS Table : Data To Procces : " + data.Count);
                int data_counter = 1;
                foreach (var d in data)
                {
                    COUT cout = new COUT();
                    cout.COUT_TYPE_BONBON = d.type_bonbon;
                    cout.COUT_FABRICATION = d.cout_fabrication;
                    cout.COUT_CONDITIONNEMENT = d.cout_conditionnement;
                    cout.COUT_EXPEDITION = d.cout_expedition;
                    cout.COUT_GENERAL = d.cout_general;

                    context.COUTS.Add(cout);
                    Console.WriteLine("-------- Data Added : (" + data_counter + "/" + data.Count + ")");
                    data_counter++;
                }

                context.SaveChanges();
                Console.WriteLine("---- Initialize COUTS Table : DONE ! ----");
            }
        }

        /// <summary>
        /// Initialize Oracle Database with Basic Data
        /// </summary>
        public static void Initialize_BaseData(bool? pauseOnEnd)
        {
            Initialize_VariantesData();
            Initialize_ConditionnementData();
            Initialize_TransportInfoData();
            
            Initialize_PaysData();
            Initialize_BonbonData();
            Initialize_Pays_RatioCommandes();
            Initialize_CartonInfos();
            Initialize_Recettes();
            Initialize_PrixLots();
            
            Initialize_TauxVentes();
            Initialize_Couts();

            if (pauseOnEnd.Value)
                Console.ReadKey(true);
        }
        #endregion

        #region OrderGeneration
        public static void GenerateOrders()
        {
            var configData = GetConfigOrderGenerationDataJSON();

            int total_orders = (int)configData.nb_commandes.Value;
            bool useRandom_countries = configData.useRandom_countries.Value;
            bool useRandom_candies = configData.useRandom_candies.Value;

            List<COMMANDE> _generated_orders = new List<COMMANDE>();
            Dictionary<PAYS, int> Countries_OrdersCount = new Dictionary<PAYS, int>();

            Console.WriteLine("---- Starting ORDERS GENERATION");
            
            using (var context = new PalaisDuBonbonEntities())
            {
                foreach(PAYS p in context.PAYS)
                {
                    PAYS pays = p;
                    int ordersCount = (int)(p.RATIO_COMMANDE * total_orders) / 100;
                    Countries_OrdersCount.Add(p, ordersCount);
                }

                if (!useRandom_countries)
                {
                    foreach(KeyValuePair<PAYS, int> pays in Countries_OrdersCount)
                    {
                        for (int i = 0; i < pays.Value; i++) // pays.value = nb de cmds d'un pays
                        {
                            int num_commande = rnd.Next(100000, 999999);
                            DateTime date_commande = DateTime.Now;
                            int nb_lots = rnd.Next((int)configData.lots_min.Value, (int)configData.lots_max.Value); // nb de lots a generer

                            for(int n = 0; n < nb_lots; n++)
                            {
                                COMMANDE commande = new COMMANDE();

                                commande.COMMANDE_NUM_COMMANDE = num_commande;
                                commande.COMMANDE_ETAT = false;
                                commande.COMMANDE_DATE_COMMANDE = date_commande;
                                commande.COMMANDE_DATE_FABRICATION = null;
                                commande.COMMANDE_DATE_CONDITIONNEMENT = null;
                                commande.COMMANDE_DATE_EXPEDITION = null;
                                commande.PAYS_ID = pays.Key.PAYS_ID;
                                commande.PAY = pays.Key;
                                commande.COMMANDE_QUANTITE = rnd.Next(nb_lots);

                                int cond_count = context.CONDITIONNEMENTs.Count();
                                int rndConditionnement = rnd.Next(1,cond_count + 1);
                                commande.CONDITIONNEMENT = context.CONDITIONNEMENTs.Where(cd => cd.CONDITIONNEMENT_ID == rndConditionnement).First();
                                commande.CONDITIONNEMENT_ID = commande.CONDITIONNEMENT.CONDITIONNEMENT_ID;

                                int randomBonbon = rnd.Next(0, 101);

                                var bonbon_probas = GetFactoryDataJSON().taux_ventes_2016;
                                int selected_bonbon = 0;
                                int selected_bonbon_id = 0;
                                foreach(var prob in bonbon_probas)
                                {
                                    selected_bonbon_id++;
                                    int val = prob.valeur;
                                    selected_bonbon += val;
                                    if(selected_bonbon >= randomBonbon)
                                    {
                                        break;
                                    }
                                }

                                commande.BONBON = context.BONBONs.Where(b => b.BONBON_ID == selected_bonbon_id).First();
                                commande.BONBON_ID = commande.BONBON.BONBON_ID;

                                PRIX_LOT test = context.PRIX_LOT.Where(pl => pl.CONDITIONNEMENT_ID == commande.CONDITIONNEMENT.CONDITIONNEMENT_ID).Single(pl => pl.BONBON.BONBON_TYPE == commande.BONBON.BONBON_TYPE);


                                commande.COMMANDE_PRIX_LOT = commande.COMMANDE_QUANTITE * test.PRIX_LOT_PRIX;

                                _generated_orders.Add(commande);
                                Console.WriteLine("-------- Order Generated");
                            }
                        } 
                    }
                }

                int counter = 0;
                foreach(COMMANDE com in _generated_orders)
                {
                    Console.WriteLine("-------- Pushing Order No (" + counter + "/" + _generated_orders.Count + 1 + ")");
                    context.COMMANDEs.Add(com);
                    if((counter % 100 ) == 0)
                    {
                        Console.WriteLine("-------- Saving changes....");
                        context.SaveChanges();
                    }
                    counter++;
                }
                Console.WriteLine("-------- Saving changes....");
                context.SaveChanges();
                Console.WriteLine("---- ORDERS GENERATION DONE");
                Console.WriteLine("---- WRITING GENERATED DATA TO generated_orders.json");
                string jsonCommandes = JsonConvert.SerializeObject(_generated_orders);
                GenerateOrdersJSONFile(jsonCommandes);
                Console.WriteLine("---- CLOSING CONNEXION...");
            }
            
            Console.WriteLine("---- WRITING DONE ! END OF FUNC");
        }

        public static void GenerateOrders_RandomRepartition(int nb, int lots_min, int lots_max)
        {
            for (int i = 0; i < nb; i++)
            {
                int _numCmd = rnd.Next(100000, 999999);
                int nb_lots = rnd.Next(lots_min, lots_max);

                PAYS _pays;
                DateTime cmd_time = DateTime.Now;
                using (var context = new PalaisDuBonbonEntities())
                {
                    int id_pays = rnd.Next(1, context.PAYS.Count());
                    _pays = context.PAYS.FirstOrDefault(p => p.PAYS_ID == id_pays);
                }

                    for (int j = 0; j < nb_lots; j++)
                    {

                    }
            }
        }
        #endregion
    }




}

