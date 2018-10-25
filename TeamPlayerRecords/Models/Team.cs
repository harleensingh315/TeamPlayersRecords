namespace TeamPlayerRecords.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Team()
        {
            TeamPlayers = new HashSet<TeamPlayer>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string TeamName { get; set; }

        [StringLength(255)]
        public string CoachName { get; set; }

        public int? Ratings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeamPlayer> TeamPlayers { get; set; }
    }
}
