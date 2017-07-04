namespace pdb_data_generator
{
    partial class CFG_CommandesGenerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_nb_commandes = new System.Windows.Forms.Label();
            this.lbl_random_rapartition = new System.Windows.Forms.Label();
            this.lbl_lots_min = new System.Windows.Forms.Label();
            this.lbl_lots_max = new System.Windows.Forms.Label();
            this.tb_nb_commandes = new System.Windows.Forms.TextBox();
            this.tb_lots_min = new System.Windows.Forms.TextBox();
            this.tb_lots_max = new System.Windows.Forms.TextBox();
            this.chkbox_random_repartition = new System.Windows.Forms.CheckBox();
            this.btn_save_json = new System.Windows.Forms.Button();
            this.btn_push_db = new System.Windows.Forms.Button();
            this.lbl_file_generated = new System.Windows.Forms.Label();
            this.lbl_done_db = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_nb_commandes
            // 
            this.lbl_nb_commandes.AutoSize = true;
            this.lbl_nb_commandes.Location = new System.Drawing.Point(12, 20);
            this.lbl_nb_commandes.Name = "lbl_nb_commandes";
            this.lbl_nb_commandes.Size = new System.Drawing.Size(126, 13);
            this.lbl_nb_commandes.TabIndex = 0;
            this.lbl_nb_commandes.Text = "Nombre de Commandes :";
            // 
            // lbl_random_rapartition
            // 
            this.lbl_random_rapartition.AutoSize = true;
            this.lbl_random_rapartition.Location = new System.Drawing.Point(12, 58);
            this.lbl_random_rapartition.Name = "lbl_random_rapartition";
            this.lbl_random_rapartition.Size = new System.Drawing.Size(108, 13);
            this.lbl_random_rapartition.TabIndex = 1;
            this.lbl_random_rapartition.Text = "Répartition Aléatoire :";
            // 
            // lbl_lots_min
            // 
            this.lbl_lots_min.AutoSize = true;
            this.lbl_lots_min.Location = new System.Drawing.Point(12, 96);
            this.lbl_lots_min.Name = "lbl_lots_min";
            this.lbl_lots_min.Size = new System.Drawing.Size(132, 13);
            this.lbl_lots_min.TabIndex = 2;
            this.lbl_lots_min.Text = "Nombre de Lots Minimum :";
            // 
            // lbl_lots_max
            // 
            this.lbl_lots_max.AutoSize = true;
            this.lbl_lots_max.Location = new System.Drawing.Point(12, 132);
            this.lbl_lots_max.Name = "lbl_lots_max";
            this.lbl_lots_max.Size = new System.Drawing.Size(135, 13);
            this.lbl_lots_max.TabIndex = 3;
            this.lbl_lots_max.Text = "Nombre de Lots Maximum :";
            // 
            // tb_nb_commandes
            // 
            this.tb_nb_commandes.Location = new System.Drawing.Point(144, 17);
            this.tb_nb_commandes.Name = "tb_nb_commandes";
            this.tb_nb_commandes.Size = new System.Drawing.Size(56, 20);
            this.tb_nb_commandes.TabIndex = 4;
            this.tb_nb_commandes.Text = "1";
            this.tb_nb_commandes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tb_lots_min
            // 
            this.tb_lots_min.Location = new System.Drawing.Point(150, 93);
            this.tb_lots_min.Name = "tb_lots_min";
            this.tb_lots_min.Size = new System.Drawing.Size(56, 20);
            this.tb_lots_min.TabIndex = 5;
            this.tb_lots_min.Text = "1";
            this.tb_lots_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tb_lots_max
            // 
            this.tb_lots_max.Location = new System.Drawing.Point(153, 129);
            this.tb_lots_max.Name = "tb_lots_max";
            this.tb_lots_max.Size = new System.Drawing.Size(56, 20);
            this.tb_lots_max.TabIndex = 6;
            this.tb_lots_max.Text = "1000";
            this.tb_lots_max.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkbox_random_repartition
            // 
            this.chkbox_random_repartition.AutoSize = true;
            this.chkbox_random_repartition.Location = new System.Drawing.Point(126, 57);
            this.chkbox_random_repartition.Name = "chkbox_random_repartition";
            this.chkbox_random_repartition.Size = new System.Drawing.Size(15, 14);
            this.chkbox_random_repartition.TabIndex = 7;
            this.chkbox_random_repartition.UseVisualStyleBackColor = true;
            // 
            // btn_save_json
            // 
            this.btn_save_json.Location = new System.Drawing.Point(74, 177);
            this.btn_save_json.Name = "btn_save_json";
            this.btn_save_json.Size = new System.Drawing.Size(96, 23);
            this.btn_save_json.TabIndex = 8;
            this.btn_save_json.Text = "Sauvegarder";
            this.btn_save_json.UseVisualStyleBackColor = true;
            this.btn_save_json.Click += new System.EventHandler(this.btn_save_json_Click);
            // 
            // btn_push_db
            // 
            this.btn_push_db.Location = new System.Drawing.Point(64, 216);
            this.btn_push_db.Name = "btn_push_db";
            this.btn_push_db.Size = new System.Drawing.Size(115, 23);
            this.btn_push_db.TabIndex = 9;
            this.btn_push_db.Text = "Push to Database";
            this.btn_push_db.UseVisualStyleBackColor = true;
            this.btn_push_db.Click += new System.EventHandler(this.btn_push_db_Click);
            // 
            // lbl_file_generated
            // 
            this.lbl_file_generated.AutoSize = true;
            this.lbl_file_generated.Location = new System.Drawing.Point(176, 182);
            this.lbl_file_generated.Name = "lbl_file_generated";
            this.lbl_file_generated.Size = new System.Drawing.Size(0, 13);
            this.lbl_file_generated.TabIndex = 10;
            // 
            // lbl_done_db
            // 
            this.lbl_done_db.AutoSize = true;
            this.lbl_done_db.Location = new System.Drawing.Point(185, 221);
            this.lbl_done_db.Name = "lbl_done_db";
            this.lbl_done_db.Size = new System.Drawing.Size(0, 13);
            this.lbl_done_db.TabIndex = 11;
            // 
            // CFG_CommandesGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 251);
            this.Controls.Add(this.lbl_done_db);
            this.Controls.Add(this.lbl_file_generated);
            this.Controls.Add(this.btn_push_db);
            this.Controls.Add(this.btn_save_json);
            this.Controls.Add(this.chkbox_random_repartition);
            this.Controls.Add(this.tb_lots_max);
            this.Controls.Add(this.tb_lots_min);
            this.Controls.Add(this.tb_nb_commandes);
            this.Controls.Add(this.lbl_lots_max);
            this.Controls.Add(this.lbl_lots_min);
            this.Controls.Add(this.lbl_random_rapartition);
            this.Controls.Add(this.lbl_nb_commandes);
            this.Name = "CFG_CommandesGenerator";
            this.Text = "Order Config.";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_nb_commandes;
        private System.Windows.Forms.Label lbl_random_rapartition;
        private System.Windows.Forms.Label lbl_lots_min;
        private System.Windows.Forms.Label lbl_lots_max;
        private System.Windows.Forms.TextBox tb_nb_commandes;
        private System.Windows.Forms.TextBox tb_lots_min;
        private System.Windows.Forms.TextBox tb_lots_max;
        private System.Windows.Forms.CheckBox chkbox_random_repartition;
        private System.Windows.Forms.Button btn_save_json;
        private System.Windows.Forms.Button btn_push_db;
        private System.Windows.Forms.Label lbl_file_generated;
        private System.Windows.Forms.Label lbl_done_db;
    }
}