//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InchikDiplomchik.ApplicatModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            this.Order = new HashSet<Order>();
        }
    
        public int ID_service { get; set; }
        public string NameService { get; set; }
        public Nullable<int> Id_discount { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }
        public byte[] Photo34 { get; set; }
    
        public virtual DISCOUN DISCOUN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }

        public int Stom
        {
            get
            {
                if (Id_discount != null)
                {
                    return Cost - ((Cost * DISCOUN.NAME_DISC) / 100);
                }
                return Cost;

            }
        }
    }
}