namespace E_CommerceWebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Images
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Images()
        {
            Brands = new HashSet<Brands>();
            Category = new HashSet<Category>();
        }

        public int Id { get; set; }

        [StringLength(250)]
        public string Large { get; set; }

        [StringLength(250)]
        public string Medium { get; set; }

        [StringLength(250)]
        public string Small { get; set; }

        public bool? Default { get; set; }

        public int? RowNumber { get; set; }

        public int? ProductID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Brands> Brands { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category> Category { get; set; }

        public virtual Products Products { get; set; }
    }
}
