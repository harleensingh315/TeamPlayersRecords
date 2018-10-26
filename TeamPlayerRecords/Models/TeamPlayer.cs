namespace TeamPlayerRecords.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TeamPlayer
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public int? TeamID { get; set; }

        public virtual Team Team { get; set; }
    }
}
