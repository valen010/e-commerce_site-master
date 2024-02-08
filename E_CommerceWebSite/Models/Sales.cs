namespace E_CommerceWebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sales
    {
        public int Id { get; set; }

        public int? ClientID { get; set; }

        public DateTime? Date { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalAmount { get; set; }

        public bool? IsInCart { get; set; }

        public int? CargoID { get; set; }

        public int? OrderStateID { get; set; }

        public int? CargoTrackID { get; set; }

        public virtual Cargo Cargo { get; set; }

        public virtual Client Client { get; set; }

        public virtual OrderState OrderState { get; set; }
    }
}
