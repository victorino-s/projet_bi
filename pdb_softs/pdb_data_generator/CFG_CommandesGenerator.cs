using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pdb_data_generator
{
    public partial class CFG_CommandesGenerator : Form
    {
        List<COMMANDE> _commandes = new List<COMMANDE>();
        int count_order_value = 1;
        bool random_repartition = false;
        int lots_min_value = 1;
        int lots_max_value = 1000;


        public CFG_CommandesGenerator()
        {
            InitializeComponent();
        }

        private void btn_save_json_Click(object sender, EventArgs e)
        {

        }

        private void btn_push_db_Click(object sender, EventArgs e)
        {

        }
    }
}
