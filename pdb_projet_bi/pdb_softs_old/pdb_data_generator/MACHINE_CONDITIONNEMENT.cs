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
    
    public partial class MACHINE_CONDITIONNEMENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MACHINE_CONDITIONNEMENT()
        {
            this.CONDITIONNEMENTs = new HashSet<CONDITIONNEMENT>();
        }
    
        public int MACHINE_CONDITIONNEMENT_ID { get; set; }
        public Nullable<int> MACHINE_CONDITIONNEMENT_CADENCE { get; set; }
        public Nullable<int> MACHINE_CONDITIONNEMENT_CHANGEMENT_OUTIL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONDITIONNEMENT> CONDITIONNEMENTs { get; set; }
    }
}
