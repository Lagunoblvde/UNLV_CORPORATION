//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UNLV_CORPORATION
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientStatus()
        {
            this.Clients = new HashSet<Clients>();
        }
    
        public int ID_StatusC { get; set; }
        public string NameStatusC { get; set; }
        public int Stocks_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Clients> Clients {private get; set; }
        public virtual Stocks Stocks {private get; set; }
    }
}
