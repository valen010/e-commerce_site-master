namespace E_CommerceWebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserComments
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string Comment { get; set; }

        public int? ClientID { get; set; }

        public bool? IsApproved { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? ProductID { get; set; }

        public virtual Client Client { get; set; }

        public virtual Products Products { get; set; }
    }
}
