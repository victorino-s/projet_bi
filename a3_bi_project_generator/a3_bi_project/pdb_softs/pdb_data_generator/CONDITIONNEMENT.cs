//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class CONDITIONNEMENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CONDITIONNEMENT()
        {
            this.COMMANDEs = new HashSet<COMMANDE>();
        }
    
        public int CONDITIONNEMENT_ID { get; set; }
        public Nullable<int> CONDITIONNEMENT_QUANTITE_MAX { get; set; }
        public Nullable<int> MACHINE_CONDITIONNEMENT_ID { get; set; }
        public string CONDITIONNEMENT_NOM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMANDE> COMMANDEs { get; set; }
        public virtual MACHINE_CONDITIONNEMENT MACHINE_CONDITIONNEMENT { get; set; }
    }
}
