using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdb_data_generator
{
    public class MachineConditionnement
    {
        public int _id { get; set; }
        public string type { get; set; }
        public int cadence { get; set; }
        public int changement_outil { get; set; }

        public bool inUse = false;
        public DateTime end_date_last_order;

        public COMMANDE cmd;
        public List<COMMANDE> processedCmds = new List<COMMANDE>();
        public List<PERF_COMMANDES> savedPerf = new List<PERF_COMMANDES>();

        string lastUsedType = "";

        public MachineConditionnement(int id, string type, int cadence, int changement_outil)
        {
            this._id = id;
            this.type = type;
            this.cadence = cadence;
            this.changement_outil = changement_outil;
            this.end_date_last_order = DateTime.Now;
        }

        public bool IsCompatibleWith(COMMANDE cmd)
        {
            bool isCompatible = false;
            using (var context = new PalaisDuBonbonEntities())
            {
                string cmd_cond = context.CONDITIONNEMENTs.Where(c => c.CONDITIONNEMENT_ID == cmd.CONDITIONNEMENT_ID).FirstOrDefault().CONDITIONNEMENT_NOM;
                if(type == cmd_cond)
                {
                    isCompatible = true;
                }
                return isCompatible;
            }
        }

        public void PickOrder(COMMANDE order, ref List<COMMANDE> to_process)
        {
            this.cmd = order;
            to_process.Remove(order);
            ProcessOrder();
        }
        /*
        void ProcessOrder()
        {
            if(!inUse)
            {
                inUse = true;
                if(cmd.COMMANDE_QUANTITE == 0) { return; }

                using (var context = new PalaisDuBonbonEntities())
                {
                    DateTime entryDate = (DateTime)context.COMMANDEs.Where(c => c.COMMANDE_ID == cmd.COMMANDE_ID).FirstOrDefault().COMMANDE_DATE_FABRICATION;
                    end_date_last_order = entryDate;
                    string bonbon_type = context.BONBONs.Where(b => b.BONBON_ID == cmd.BONBON_ID).FirstOrDefault().BONBON_TYPE;

                    if(!(bonbon_type == lastUsedType))
                    {
                        lastUsedType = bonbon_type;
                        end_date_last_order = end_date_last_order.AddMinutes(this.changement_outil);
                    }

                    int qte_lots = (int)cmd.COMMANDE_QUANTITE;
                    double tps_traitement = (double)qte_lots / ((double)this.cadence / 60.0f);
                    end_date_last_order = end_date_last_order.AddMinutes(tps_traitement);

                    PERF_COMMANDES pc = context.PERF_COMMANDES.Where(p => p.COMMANDE_ID == cmd.COMMANDE_ID).FirstOrDefault();
                    pc.TEMPS_CONDITIONNEMENT = (decimal)tps_traitement;
                    pc.PERF_MACHINE_CONDITIONNEMENT_ID = _id;
                    savedPerf.Add(pc);

                    cmd.COMMANDE_DATE_CONDITIONNEMENT = end_date_last_order;
                    processedCmds.Add(cmd);
                }
                inUse = false;
            }
        }*/

        void ProcessOrder()
        {
            if (!inUse)
            {
                inUse = true;
                if (cmd.COMMANDE_QUANTITE == 0) { return; }
                DateTime startProcessDate = end_date_last_order;
                using (var context = new PalaisDuBonbonEntities())
                {
                    DateTime entryDate = (DateTime)context.COMMANDEs.Where(c => c.COMMANDE_ID == cmd.COMMANDE_ID).FirstOrDefault().COMMANDE_DATE_FABRICATION;
                    end_date_last_order = entryDate;
                    string bonbon_type = context.BONBONs.Where(b => b.BONBON_ID == cmd.BONBON_ID).FirstOrDefault().BONBON_TYPE;

                    if (!(bonbon_type == lastUsedType))
                    {
                        lastUsedType = bonbon_type;
                        end_date_last_order = end_date_last_order.AddMinutes(this.changement_outil);
                    }

                    int qte_lots = (int)cmd.COMMANDE_QUANTITE;
                    double tps_traitement = (double)qte_lots / ((double)this.cadence / 60.0f);
                    end_date_last_order = end_date_last_order.AddMinutes(tps_traitement);

                    PERF_COMMANDES pc = context.PERF_COMMANDES.Where(p => p.COMMANDE_ID == cmd.COMMANDE_ID).FirstOrDefault();
                    pc.TEMPS_CONDITIONNEMENT = (decimal)end_date_last_order.Subtract(startProcessDate).TotalMinutes;
                    pc.PERF_MACHINE_CONDITIONNEMENT_ID = _id;
                    savedPerf.Add(pc);

                    cmd.COMMANDE_DATE_CONDITIONNEMENT = end_date_last_order;
                    processedCmds.Add(cmd);
                }
                inUse = false;
            }
        }

        public void PersistPerformances()
        {
            using (var context = new PalaisDuBonbonEntities())
            {
                foreach(COMMANDE c in processedCmds)
                {
                    context.COMMANDEs.Where(com => com.COMMANDE_ID == c.COMMANDE_ID).FirstOrDefault().COMMANDE_DATE_CONDITIONNEMENT = c.COMMANDE_DATE_CONDITIONNEMENT;
                }

                foreach(PERF_COMMANDES pc in savedPerf)
                {
                    context.PERF_COMMANDES.Where(p => p.COMMANDE_ID == pc.COMMANDE_ID).FirstOrDefault().TEMPS_CONDITIONNEMENT = pc.TEMPS_CONDITIONNEMENT;
                    context.PERF_COMMANDES.Where(p => p.COMMANDE_ID == pc.COMMANDE_ID).FirstOrDefault().PERF_MACHINE_CONDITIONNEMENT_ID = pc.PERF_MACHINE_CONDITIONNEMENT_ID;
                }

                context.SaveChanges();
                Console.WriteLine("Conditionnement Performances of Machine No " + _id + " SAVED IN DB !");
            }
        }
    }
}
