﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace pdb_data_generator
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PalaisDuBonbonEntities : DbContext
    {
        public PalaisDuBonbonEntities()
            : base("name=PalaisDuBonbonEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BONBON> BONBONs { get; set; }
        public virtual DbSet<CARTON> CARTONs { get; set; }
        public virtual DbSet<CARTON_INFO> CARTON_INFO { get; set; }
        public virtual DbSet<COMMANDE> COMMANDEs { get; set; }
        public virtual DbSet<CONDITIONNEMENT> CONDITIONNEMENTs { get; set; }
        public virtual DbSet<MACHINE_CONDITIONNEMENT> MACHINE_CONDITIONNEMENT { get; set; }
        public virtual DbSet<MACHINE_FABRICATION> MACHINE_FABRICATION { get; set; }
        public virtual DbSet<PAYS> PAYS { get; set; }
        public virtual DbSet<PRIX_LOT> PRIX_LOT { get; set; }
        public virtual DbSet<RECETTE> RECETTEs { get; set; }
        public virtual DbSet<TRANSPORT_INFO> TRANSPORT_INFO { get; set; }
        public virtual DbSet<VARIANTE> VARIANTEs { get; set; }
        public virtual DbSet<TAUX_VENTES> TAUX_VENTES { get; set; }
        public virtual DbSet<COUT> COUTS { get; set; }
    }
}
