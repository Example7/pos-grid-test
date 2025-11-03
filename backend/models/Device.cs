using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevExpress.Models
{
    [Table("devices")]
    public class Device
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("pos_id")]
        public string PosId { get; set; } = string.Empty;

        [Column("location")]
        public string? Location { get; set; }

        [Column("active")]
        public bool? Active { get; set; } = true;

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
