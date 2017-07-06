using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdb_data_generator
{
    public class MachineFabrication
    {
        public int _id { get; set; }
        public List<string> variantes = new List<string>();
        public List<int> cadences = new List<int>();
        public List<int> changements_outil = new List<int>();
        public DateTime end_date_last_order;
        public string last_used_candy_type = "";
        public bool inUse = false;

        PalaisDuBonbonEntities _context;

        COMMANDE cmd;
        List<COMMANDE> processedOrders = new List<COMMANDE>();
        List<PERF_COMMANDES> savedPerformances = new List<PERF_COMMANDES>();
        public MachineFabrication(int id, dynamic variantes, dynamic cadences, dynamic changements_outil)
        {
            this._id = id;
            foreach (string s in variantes)
            {
                this.variantes.Add(s);
            }
            foreach (int i in cadences)
            {
                this.cadences.Add(i);
            }

            foreach (int i in changements_outil)
            {
                this.changements_outil.Add(i);
            }
            this.inUse = false;
            this.end_date_last_order = DateTime.Now;
        }

        public MachineFabrication(int id, dynamic variantes, dynamic cadences, dynamic changements_outil, PalaisDuBonbonEntities context)
        {
            this._id = id;
            foreach (string s in variantes)
            {
                this.variantes.Add(s);
            }
            foreach (int i in cadences)
            {
                this.cadences.Add(i);
            }

            foreach (int i in changements_outil)
            {
                this.changements_outil.Add(i);
            }
            this.inUse = false;
            this.end_date_last_order = DateTime.Now;
            this._context = context;
        }

        void ProcessOrder()
        {
            
            if (!inUse)
            {
                inUse = true;
                if (cmd.COMMANDE_QUANTITE == 0) { return; }

                int qte_cond;
                DateTime startProcessDate = end_date_last_order;
                using (var context = new PalaisDuBonbonEntities())
                {
                    qte_cond = (int)context.CONDITIONNEMENTs.Where(c => c.CONDITIONNEMENT_ID == cmd.CONDITIONNEMENT_ID).FirstOrDefault().CONDITIONNEMENT_QUANTITE_MAX;

                    int quantite_bonbon = (int)(cmd.COMMANDE_QUANTITE * qte_cond);

                    string bonbon_type = context.BONBONs.Where(c => c.BONBON_ID == cmd.BONBON_ID).FirstOrDefault().BONBON_TYPE;
                    string bonbon_variante = context.BONBONs.Where(c => c.BONBON_ID == cmd.BONBON_ID).FirstOrDefault().VARIANTE.VARIANTE_NOM;
                    int varianteIndex = variantes.IndexOf(bonbon_variante);

                    if (last_used_candy_type != bonbon_type)
                    {
                        last_used_candy_type = bonbon_type;
                        end_date_last_order = end_date_last_order.AddMinutes(this.changements_outil[varianteIndex]);
                    }

                    double tps_traitement = (float)quantite_bonbon / (float)(cadences[varianteIndex] / 60); // Tps en minutes
                    end_date_last_order = end_date_last_order.AddMinutes(tps_traitement);

                    cmd.COMMANDE_DATE_FABRICATION = end_date_last_order;

                    processedOrders.Add(cmd);

                    PERF_COMMANDES perfCmd = new PERF_COMMANDES();
                    perfCmd.COMMANDE_ID = cmd.COMMANDE_ID;
                    perfCmd.TEMPS_FABRICATION_LOT = (decimal)end_date_last_order.Subtract(startProcessDate).TotalMinutes;
                    perfCmd.BONBON_FABRIQUE_COUNT = quantite_bonbon;
                    perfCmd.PERF_MACHINE_FABRICATION_ID = _id;
                    savedPerformances.Add(perfCmd);

                    /*******************************
                    *            DEBUG
                    ********************************/
/*
                    dynamic jsonCmd = new System.Dynamic.ExpandoObject();
                    jsonCmd.machine = _id;
                    jsonCmd.date = cmd.COMMANDE_DATE_FABRICATION;
                    jsonCmd.id = cmd.COMMANDE_ID;
                    jsonCmd.numCmd = cmd.COMMANDE_NUM_COMMANDE;
                    jsonCmd.qte_bonbon = quantite_bonbon;
                    jsonCmd.temps_fab_lot = tps_traitement;

                    string jsonData = JsonConvert.SerializeObject(jsonCmd);
                    Utils.GenerateFabricationData_Test(jsonData);
*/
                    /*******************************
                    *            END DEBUG
                    ********************************/
                }
                inUse = false;
            }

            //Console.ReadLine();
        }

        void ProcessOrderOpti()
        {
            if (!inUse)
            {
                DateTime startProcessDate = end_date_last_order;
                inUse = true;
                if (cmd.COMMANDE_QUANTITE == 0) { return; }

                int qte_cond;

                    qte_cond = (int)_context.CONDITIONNEMENTs.Where(c => c.CONDITIONNEMENT_ID == cmd.CONDITIONNEMENT_ID).FirstOrDefault().CONDITIONNEMENT_QUANTITE_MAX;

                    int quantite_bonbon = (int)(cmd.COMMANDE_QUANTITE * qte_cond);

                    string bonbon_type = _context.BONBONs.Where(c => c.BONBON_ID == cmd.BONBON_ID).FirstOrDefault().BONBON_TYPE;
                    string bonbon_variante = _context.BONBONs.Where(c => c.BONBON_ID == cmd.BONBON_ID).FirstOrDefault().VARIANTE.VARIANTE_NOM;
                    int varianteIndex = variantes.IndexOf(bonbon_variante);

                    if (!(bonbon_type == last_used_candy_type))
                    {
                        last_used_candy_type = bonbon_type;
                        end_date_last_order = end_date_last_order.AddMinutes(this.changements_outil[varianteIndex]);
                    }

                    double tps_traitement = (float)quantite_bonbon / (float)(cadences[varianteIndex] / 60); // Tps en minutes
                    end_date_last_order = end_date_last_order.AddMinutes(tps_traitement);

                    cmd.COMMANDE_DATE_FABRICATION = end_date_last_order;

                    processedOrders.Add(cmd);

                    PERF_COMMANDES perfCmd = new PERF_COMMANDES();
                    perfCmd.COMMANDE_ID = cmd.COMMANDE_ID;
                    perfCmd.TEMPS_FABRICATION_LOT = (decimal)end_date_last_order.Subtract(startProcessDate).TotalMinutes;
                    perfCmd.BONBON_FABRIQUE_COUNT = quantite_bonbon;
                    perfCmd.PERF_MACHINE_FABRICATION_ID = _id;
                    savedPerformances.Add(perfCmd);

                    /*******************************
                    *            DEBUG
                    ********************************/
                    /*
                                        dynamic jsonCmd = new System.Dynamic.ExpandoObject();
                                        jsonCmd.machine = _id;
                                        jsonCmd.date = cmd.COMMANDE_DATE_FABRICATION;
                                        jsonCmd.id = cmd.COMMANDE_ID;
                                        jsonCmd.numCmd = cmd.COMMANDE_NUM_COMMANDE;
                                        jsonCmd.qte_bonbon = quantite_bonbon;
                                        jsonCmd.temps_fab_lot = tps_traitement;

                                        string jsonData = JsonConvert.SerializeObject(jsonCmd);
                                        Utils.GenerateFabricationData_Test(jsonData);
                    */
                    /*******************************
                    *            END DEBUG
                    ********************************/
                
                inUse = false;
            }

            //Console.ReadLine();
        }



        public bool IsCompatibleWith(COMMANDE cmd)
        {
            bool isCompatible = false;
            using (var context = new PalaisDuBonbonEntities())
            {
                string bonbon_variante = context.BONBONs.Where(b => b.BONBON_ID == cmd.BONBON_ID).FirstOrDefault().VARIANTE.VARIANTE_NOM;
                if(variantes.Contains(bonbon_variante))
                {
                    isCompatible = true;
                }
            }

            return isCompatible;
        }

        public void PickOrder(COMMANDE order, ref List<COMMANDE> to_process)
        {
            this.cmd = order;
            to_process.Remove(order);
            ProcessOrder();
        }

        public void PickOrderOpti(COMMANDE order, ref List<COMMANDE> to_process)
        {
            this.cmd = order;
            to_process.Remove(order);
            ProcessOrderOpti();
        }

        public void PersistPerformances()
        {
            using (var context = new PalaisDuBonbonEntities())
            {
                foreach(COMMANDE com in processedOrders)
                {
                    context.COMMANDEs.Where(c => c.COMMANDE_ID == com.COMMANDE_ID).FirstOrDefault().COMMANDE_DATE_FABRICATION = com.COMMANDE_DATE_FABRICATION;
                }

                foreach(PERF_COMMANDES c in savedPerformances)
                {
                    if(context.PERF_COMMANDES.Any(p => p.COMMANDE_ID == c.COMMANDE_ID))
                    {
                        PERF_COMMANDES copy = context.PERF_COMMANDES.Where(p => p.COMMANDE_ID == c.COMMANDE_ID).FirstOrDefault();
                        copy.TEMPS_FABRICATION_LOT = c.TEMPS_FABRICATION_LOT;
                        copy.PERF_MACHINE_FABRICATION_ID = c.PERF_MACHINE_FABRICATION_ID;
                        copy.PERF_MACHINE_CONDITIONNEMENT_ID = -1;
                    }
                    else
                    {
                        context.PERF_COMMANDES.Add(c);
                    }
                    
                    
                }

                context.SaveChanges();
                Console.WriteLine("Fabrication Performances of Machine No " + _id + " SAVED IN DB !");
            }
        }

        public void PersistPerformancesOpti()
        {
          
                foreach (COMMANDE com in processedOrders)
                {
                    _context.COMMANDEs.Where(c => c.COMMANDE_ID == com.COMMANDE_ID).FirstOrDefault().COMMANDE_DATE_FABRICATION = com.COMMANDE_DATE_FABRICATION;
                }

                foreach (PERF_COMMANDES c in savedPerformances)
                {
                    if (_context.PERF_COMMANDES.Any(p => p.COMMANDE_ID == c.COMMANDE_ID))
                    {
                        PERF_COMMANDES copy = _context.PERF_COMMANDES.Where(p => p.COMMANDE_ID == c.COMMANDE_ID).FirstOrDefault();
                        copy.TEMPS_FABRICATION_LOT = c.TEMPS_FABRICATION_LOT;
                        copy.PERF_MACHINE_FABRICATION_ID = c.PERF_MACHINE_FABRICATION_ID;
                        copy.PERF_MACHINE_CONDITIONNEMENT_ID = -1;
                    }
                    else
                    {
                        _context.PERF_COMMANDES.Add(c);
                    }


                }

                _context.SaveChanges();
                Console.WriteLine("Fabrication Performances of Machine No " + _id + " SAVED IN DB !");
            
        }
    }
}
