using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Dynamic;
using System.Threading;

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

        public static void GenerateFabricationData_Test(string json)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"_static_files", "generated_fab.json");
            File.AppendAllText(path, json + ",");
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

                foreach (var d in data)
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

                    if ((c % 100) == 0)
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
                foreach (var d in data)
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
                foreach (PAYS p in context.PAYS)
                {
                    PAYS pays = p;
                    int ordersCount = (int)(p.RATIO_COMMANDE * total_orders) / 100;
                    Countries_OrdersCount.Add(p, ordersCount);
                }

                if (!useRandom_countries)
                {
                    foreach (KeyValuePair<PAYS, int> pays in Countries_OrdersCount)
                    {
                        for (int i = 0; i < pays.Value; i++) // pays.value = nb de cmds d'un pays
                        {
                            int num_commande = rnd.Next(100000, 999999);
                            int data_generation_year = (int)configData.generation_fabrication_data_date;
                            DateTime date_commande = DateTime.Now;
                            int nb_lots = rnd.Next((int)configData.lots_min.Value, (int)configData.lots_max.Value); // nb de lots a generer

                            for (int n = 0; n < nb_lots; n++)
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
                                commande.COMMANDE_QUANTITE = nb_lots;

                                int cond_count = context.CONDITIONNEMENTs.Count();
                                int rndConditionnement = rnd.Next(1, cond_count + 1);
                                commande.CONDITIONNEMENT = context.CONDITIONNEMENTs.Where(cd => cd.CONDITIONNEMENT_ID == rndConditionnement).First();
                                commande.CONDITIONNEMENT_ID = commande.CONDITIONNEMENT.CONDITIONNEMENT_ID;

                                int randomBonbon = rnd.Next(0, 100);

                                var bonbon_probas = GetFactoryDataJSON().taux_ventes_2016;
                                //   int selected_bonbon = 0;
                                // int selected_bonbon_id = 0;
                                float myValue = 0f;
                                string choosed_candy_name = "";
                                foreach (var prob in bonbon_probas)
                                {
                                    float valeur = prob.valeur;
                                    myValue += valeur;
                                    if (myValue >= randomBonbon)
                                    {
                                        choosed_candy_name = prob.type_bonbon;
                                        break;
                                    }
                                    /*selected_bonbon_id++;
                                    int val = prob.valeur;
                                    selected_bonbon += val;
                                    if (selected_bonbon >= randomBonbon)
                                    {
                                        break;
                                    }*/
                                }
                                List<BONBON> bonbons_available = context.BONBONs.Where(b => b.BONBON_TYPE == choosed_candy_name).ToList();
                                commande.BONBON = bonbons_available.ElementAt(rnd.Next(bonbons_available.Count));
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
                foreach (COMMANDE com in _generated_orders)
                {
                    Console.WriteLine("-------- Pushing Order No (" + counter + "/" + _generated_orders.Count + ")");
                    context.COMMANDEs.Add(com);
                    if ((counter % 100) == 0)
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
                //  string jsonCommandes = JsonConvert.SerializeObject(_generated_orders);
                //   GenerateOrdersJSONFile(jsonCommandes);
                Console.WriteLine("---- CLOSING CONNEXION...");
            }

            Console.WriteLine("---- WRITING DONE ! END OF FUNC");
        }

        public static void Refresh_CommandesDates()
        {
            Console.WriteLine("--- Begin Refresh Commandes Dates ----");
            using (var context = new PalaisDuBonbonEntities())
            {
                int count = 0;
                int max = context.COMMANDEs.Count();
                foreach (COMMANDE com in context.COMMANDEs)
                {
                    com.COMMANDE_DATE_COMMANDE = GenerateRandomDateForYear(2016);
                    count++;
                    Console.WriteLine("-------- Process : (" + count.ToString() + "/" + max.ToString() + ")");
                }
                Console.WriteLine("--- Saving changes...");
                context.SaveChanges();
                Console.WriteLine("--- Done ! ----");
            }
        }

        public static void Drop_AllDynamicData()
        {
            Console.WriteLine("--- Begin REFRESH ALL DYNAMIC DATA ----");
            using (var context = new PalaisDuBonbonEntities())
            {
                int count_table_perf = 0;
                int max_table_perf = context.PERF_COMMANDES.Count();

                int count_table_cmd = 0;
                int max_table_cmd = context.COMMANDEs.Count();

                Console.WriteLine("--- Deleting Perf_Commandes Table");
                List<PERF_COMMANDES> cp_perf = new List<PERF_COMMANDES>(context.PERF_COMMANDES.ToList());
                context.PERF_COMMANDES.RemoveRange(cp_perf);
                //cp_perf.ForEach(perf => { context.PERF_COMMANDES.Remove(perf); Console.WriteLine("-------- perfs (" + count_table_perf+ "/" + max_table_perf + ")"); count_table_perf++; });
                Console.WriteLine("--- End");

                Console.WriteLine("--- **************************************");

                Console.WriteLine("--- Deleting Commandes Table");
                List<COMMANDE> cp_cmds = new List<COMMANDE>(context.COMMANDEs.ToList());
                context.COMMANDEs.RemoveRange(cp_cmds);
                //cp_cmds.ForEach(cmd => { context.COMMANDEs.Remove(cmd); Console.WriteLine("-------- cmds (" + count_table_cmd + "/" + max_table_cmd + ")"); count_table_cmd++; });

                Console.WriteLine("--- Saving changes...");
                context.SaveChanges();
                Console.WriteLine("--- Done ! ----");
            }
        }

        /*
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
        }*/
        #endregion

        #region Fabrication
        public static MachineFabrication BuildMachineFabricationFromJSON(int machineId)
        {
            var data = GetFactoryDataJSON().machines_fabrication[machineId];

            int id = data._id;
            var variantes = data.variante;
            var cadences = data.cadence;
            var changements_outils = data.changement_outil;

            return new MachineFabrication(id, variantes, cadences, changements_outils);
        }

        public static MachineFabrication BuildMachineFabricationFromJSON(int machineId, PalaisDuBonbonEntities context)
        {
            var data = GetFactoryDataJSON().machines_fabrication[machineId];

            int id = data._id;
            var variantes = data.variante;
            var cadences = data.cadence;
            var changements_outils = data.changement_outil;

            return new MachineFabrication(id, variantes, cadences, changements_outils, context);
        }



        public static void Build_UnprocessedOrders()
        {
            var fabMachines = GetFactoryDataJSON().machines_fabrication;
            List<COMMANDE> commandes_a_traiter;
            MachineFabrication m1;
            MachineFabrication m2;
            MachineFabrication m3;
            MachineFabrication m4;
            using (var context = new PalaisDuBonbonEntities())
            {
                commandes_a_traiter = new List<COMMANDE>(context.COMMANDEs.Where(c => c.COMMANDE_ETAT == (false)));
                commandes_a_traiter.Sort((c1, c2) => c1.COMMANDE_DATE_COMMANDE.Value.CompareTo(c2.COMMANDE_DATE_COMMANDE.Value));


                int totalCmdsCount = commandes_a_traiter.Count;
                List<COMMANDE> commandes_traites = new List<COMMANDE>();

                //
                m1 = BuildMachineFabricationFromJSON(0);
                m2 = BuildMachineFabricationFromJSON(1);
                m3 = BuildMachineFabricationFromJSON(2);
                m4 = BuildMachineFabricationFromJSON(3);

                int total_element = commandes_a_traiter.Count;
                while (commandes_a_traiter.Count > 0 || m1.inUse || m2.inUse || m3.inUse || m4.inUse)
                {
                    Console.WriteLine("-------- Remaining Elements : " + commandes_a_traiter.Count + "/" + total_element);
                    if (commandes_a_traiter[0].BONBON.VARIANTE.VARIANTE_NOM == "sucre")
                    {
                        int choixMachine = rnd.Next(0, 2);


                        if (choixMachine == 0)
                        {
                            if (!m2.inUse)
                                m2.PickOrder(commandes_a_traiter[0], ref commandes_a_traiter);
                        }
                        else
                        {
                            if (!m4.inUse)
                                m4.PickOrder(commandes_a_traiter[0], ref commandes_a_traiter);
                        }

                    }
                    else if (commandes_a_traiter[0].BONBON.VARIANTE.VARIANTE_NOM == "gelifie")
                    {
                        int choixMachine = rnd.Next(0, 2);


                        if (choixMachine == 0)
                        {
                            if (!m3.inUse)
                                m3.PickOrder(commandes_a_traiter[0], ref commandes_a_traiter);
                        }
                        else
                        {
                            if (!m4.inUse)
                                m4.PickOrder(commandes_a_traiter[0], ref commandes_a_traiter);
                        }
                    }
                    else
                    {
                        if (!m1.inUse)
                            m1.PickOrder(commandes_a_traiter[0], ref commandes_a_traiter);
                    }
                }
            }
            m1.PersistPerformances();
            m2.PersistPerformances();
            m3.PersistPerformances();
            m4.PersistPerformances();
            Console.WriteLine("---- FABRICATION DONE");
        }

        public static void Build_UnprocessedOrders_Opti()
        {
            var fabMachines = GetFactoryDataJSON().machines_fabrication;
            List<COMMANDE> commandes_a_traiter;
            MachineFabrication m1;
            MachineFabrication m2;
            MachineFabrication m3;
            MachineFabrication m4;
            using (var context = new PalaisDuBonbonEntities())
            {
                commandes_a_traiter = new List<COMMANDE>(context.COMMANDEs.Where(c => c.COMMANDE_ETAT == (false)));
                commandes_a_traiter.Sort((c1, c2) => c1.COMMANDE_DATE_COMMANDE.Value.CompareTo(c2.COMMANDE_DATE_COMMANDE.Value));

                List<COMMANDE> cmds_sucres = commandes_a_traiter.Where(c => c.BONBON.VARIANTE.VARIANTE_NOM == "sucre").ToList();
                cmds_sucres.Sort((c1, c2) => c1.BONBON.BONBON_TYPE.CompareTo(c2.BONBON.BONBON_TYPE));
                List<COMMANDE> cmds_acides = commandes_a_traiter.Where(c => c.BONBON.VARIANTE.VARIANTE_NOM == "acide").ToList();
                cmds_acides.Sort((c1, c2) => c1.BONBON.BONBON_TYPE.CompareTo(c2.BONBON.BONBON_TYPE));
                List<COMMANDE> cmds_gelifies = commandes_a_traiter.Where(c => c.BONBON.VARIANTE.VARIANTE_NOM == "gelifie").ToList();
                cmds_gelifies.Sort((c1, c2) => c1.BONBON.BONBON_TYPE.CompareTo(c2.BONBON.BONBON_TYPE));

                int totalCmdsCount = cmds_acides.Count + cmds_gelifies.Count + cmds_sucres.Count;

                //
                m1 = BuildMachineFabricationFromJSON(0);
                m2 = BuildMachineFabricationFromJSON(1);
                m3 = BuildMachineFabricationFromJSON(2);
                m4 = BuildMachineFabricationFromJSON(3);

                while (cmds_sucres.Count > 0)
                {
                    int choixMachine = rnd.Next(0, 2);


                    if (choixMachine == 0)
                    {
                        if (!m2.inUse)
                            m2.PickOrder(cmds_sucres[0], ref cmds_sucres);
                    }
                    else
                    {
                        if (!m4.inUse)
                            m4.PickOrder(cmds_sucres[0], ref cmds_sucres);
                    }
                }

                while (cmds_gelifies.Count > 0)
                {
                    int choixMachine = rnd.Next(0, 2);


                    if (choixMachine == 0)
                    {
                        if (!m3.inUse)
                            m3.PickOrder(cmds_gelifies[0], ref cmds_gelifies);
                        else
                        {
                            if (!m4.inUse)
                                m4.PickOrder(cmds_gelifies[0], ref cmds_gelifies);
                        }
                    }
                }
                while (cmds_acides.Count > 0)
                {
                    if (!m1.inUse)
                        m1.PickOrder(cmds_acides[0], ref cmds_acides);
                }
            }
            m1.PersistPerformances();
            m2.PersistPerformances();
            m3.PersistPerformances();
            m4.PersistPerformances();


            Console.WriteLine("---- FABRICATION DONE");
        }
        #endregion

        #region Conditionnement
        public static MachineConditionnement BuilMachineConditionnementFromJSON(int machineId)
        {
            var data = GetFactoryDataJSON().machines_conditionnement[machineId];

            int id = data._id;
            string conditionnement = data.conditionnement;
            int cadence = data.cadence;
            int changement_outil = data.changement_outil;

            return new MachineConditionnement(id, conditionnement, cadence, changement_outil);
        }

        public static void Package_UnprocessedOrders()
        {
            List<COMMANDE> commandes_a_traiter;

            MachineConditionnement m1;
            MachineConditionnement m2;
            MachineConditionnement m3;
            MachineConditionnement m4;
            MachineConditionnement m5;
            MachineConditionnement m6;
            using (var context = new PalaisDuBonbonEntities())
            {
                commandes_a_traiter = new List<COMMANDE>(context.COMMANDEs.Where(c => c.COMMANDE_ETAT == (false)));
                commandes_a_traiter.Sort((c1, c2) => c1.COMMANDE_DATE_FABRICATION.Value.CompareTo(c2.COMMANDE_DATE_FABRICATION.Value));


                m1 = BuilMachineConditionnementFromJSON(0);
                m2 = BuilMachineConditionnementFromJSON(1);

                m3 = BuilMachineConditionnementFromJSON(2);
                m4 = BuilMachineConditionnementFromJSON(3);

                m5 = BuilMachineConditionnementFromJSON(4);
                m6 = BuilMachineConditionnementFromJSON(5);

                int total_element = commandes_a_traiter.Count;
                while (commandes_a_traiter.Count > 0 || m1.inUse || m2.inUse || m3.inUse || m4.inUse || m5.inUse || m6.inUse)
                {
                    Console.WriteLine("-------- Remaining Elements : " + commandes_a_traiter.Count + "/" + total_element);

                    if (commandes_a_traiter[0].CONDITIONNEMENT.CONDITIONNEMENT_NOM == "sachet")
                    {
                        int choixMachine = rnd.Next(0, 3);
                        if (choixMachine == 0)
                        {
                            m1.PickOrder(commandes_a_traiter[0], ref commandes_a_traiter);
                        }
                        else if (choixMachine == 1)
                        {
                            m2.PickOrder(commandes_a_traiter[0], ref commandes_a_traiter);
                        }
                        else
                        {
                            m3.PickOrder(commandes_a_traiter[0], ref commandes_a_traiter);
                        }
                    }
                    else if (commandes_a_traiter[0].CONDITIONNEMENT.CONDITIONNEMENT_NOM == "boite")
                    {
                        int choixMachine = rnd.Next(0, 2);

                        if (choixMachine == 0)
                        {
                            m4.PickOrder(commandes_a_traiter[0], ref commandes_a_traiter);
                        }
                        else
                        {
                            m5.PickOrder(commandes_a_traiter[0], ref commandes_a_traiter);
                        }

                    }
                    else
                    {
                        if (!m6.inUse)
                            m6.PickOrder(commandes_a_traiter[0], ref commandes_a_traiter);
                    }
                }
            }
            m1.PersistPerformances();
            m2.PersistPerformances();
            m3.PersistPerformances();
            m4.PersistPerformances();
            m5.PersistPerformances();
            m6.PersistPerformances();
            Console.WriteLine("---- CONDITIONNEMENT DONE");

        }

        public static void Package_UnprocessedOrders_Opti()
        {
            List<COMMANDE> commandes_a_traiter;

            MachineConditionnement m1;
            MachineConditionnement m2;
            MachineConditionnement m3;
            MachineConditionnement m4;
            MachineConditionnement m5;
            MachineConditionnement m6;
            using (var context = new PalaisDuBonbonEntities())
            {
                commandes_a_traiter = new List<COMMANDE>(context.COMMANDEs.Where(c => c.COMMANDE_ETAT == (false)));
                commandes_a_traiter.Sort((c1, c2) => c1.COMMANDE_DATE_FABRICATION.Value.CompareTo(c2.COMMANDE_DATE_FABRICATION.Value));

                List<COMMANDE> cmds_sachets = commandes_a_traiter.Where(c => c.CONDITIONNEMENT.CONDITIONNEMENT_NOM == "sachet").ToList();
                cmds_sachets.Sort((c1, c2) => c1.BONBON.BONBON_TYPE.CompareTo(c2.BONBON.BONBON_TYPE));
                List<COMMANDE> cmds_boites = commandes_a_traiter.Where(c => c.CONDITIONNEMENT.CONDITIONNEMENT_NOM == "boite").ToList();
                cmds_boites.Sort((c1, c2) => c1.BONBON.BONBON_TYPE.CompareTo(c2.BONBON.BONBON_TYPE));
                List<COMMANDE> cmds_echantillons = commandes_a_traiter.Where(c => c.CONDITIONNEMENT.CONDITIONNEMENT_NOM == "echantillon").ToList();
                cmds_echantillons.Sort((c1, c2) => c1.BONBON.BONBON_TYPE.CompareTo(c2.BONBON.BONBON_TYPE));

                m1 = BuilMachineConditionnementFromJSON(0);
                m2 = BuilMachineConditionnementFromJSON(1);

                m3 = BuilMachineConditionnementFromJSON(2);
                m4 = BuilMachineConditionnementFromJSON(3);

                m5 = BuilMachineConditionnementFromJSON(4);
                m6 = BuilMachineConditionnementFromJSON(5);

                while(cmds_sachets.Count > 0)
                {
                    if(cmds_sachets.Count >= 1)
                    {
                        if (!m1.inUse)
                            m1.PickOrder(cmds_sachets[0], ref cmds_sachets);
                    }

                    if(cmds_sachets.Count >= 2)
                    {
                        if (!m2.inUse)
                            m2.PickOrder(cmds_sachets[1], ref cmds_sachets);
                    }

                    if (cmds_sachets.Count >= 3)
                    {
                        if (!m3.inUse)
                            m3.PickOrder(cmds_sachets[2], ref cmds_sachets);
                    }
                }

                while(cmds_boites.Count > 0)
                {
                    if (cmds_boites.Count >= 1)
                    {
                        if (!m4.inUse)
                            m4.PickOrder(cmds_boites[0], ref cmds_boites);
                    }

                    if (cmds_boites.Count >= 2)
                    {
                        if (!m5.inUse)
                            m5.PickOrder(cmds_boites[1], ref cmds_boites);
                    }
                }

                while (cmds_echantillons.Count > 0)
                {
                    
                        if (!m6.inUse)
                            m6.PickOrder(cmds_echantillons[0], ref cmds_echantillons);
                }
            }
            m1.PersistPerformances();
            m2.PersistPerformances();
            m3.PersistPerformances();
            m4.PersistPerformances();
            m5.PersistPerformances();
            m6.PersistPerformances();
            Console.WriteLine("---- CONDITIONNEMENT DONE");

        }
        #endregion

        #region MongoUtils
        public static void Generate_Data_001()
        {
            List<object> objs = new List<object>();
            using (var context = new PalaisDuBonbonEntities())
            {
                Console.WriteLine("---- Getting Data - " + context.COMMANDEs.Count() + " rows to process");
                foreach (COMMANDE c in context.COMMANDEs)
                {
                    string bonbon_type = context.BONBONs.Where(t => t.BONBON_ID == c.BONBON_ID).FirstOrDefault().BONBON_TYPE;
                    int quantite = (int)c.COMMANDE_QUANTITE;
                    string pays = context.PAYS.Where(p => p.PAYS_ID == c.PAYS_ID).FirstOrDefault().PAYS_NOM;

                    dynamic dyn = new ExpandoObject();
                    dyn.bonbon_type = bonbon_type;
                    dyn.quantite = quantite;
                    dyn.pays_nom = pays;
                    objs.Add(dyn);
                }
            }

            string path = Path.Combine(Environment.CurrentDirectory, @"_static_files", "mongoData_kpi_001.json");
            string json = JsonConvert.SerializeObject(objs);
            File.WriteAllText(path, json);
            Console.WriteLine("--- Writing DONE !");
        }
        #endregion

        #region Misc
        public static DateTime GenerateRandomDateForYear(int year)
        {
            DateTime start = new DateTime(year, 1, 1);
            DateTime end = new DateTime(year, 12, 31);
            int range = (end - start).Days;
            return start.AddDays(rnd.Next(range));
        }
        #endregion
    }




}

