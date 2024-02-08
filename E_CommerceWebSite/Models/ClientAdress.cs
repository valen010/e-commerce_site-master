namespace E_CommerceWebSite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClientAdress")]
    public partial class ClientAdress
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? ClientID { get; set; }

        [StringLength(150)]
        public string Adress { get; set; }

        public virtual Client Client { get; set; }
    }
}
