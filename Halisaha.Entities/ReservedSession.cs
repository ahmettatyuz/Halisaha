using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Halisaha.Entities
{
	public class ReservedSession
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public int SessionId { get; set; }

        [JsonIgnore]
        public Session? Session { get; set; }

        [Required]
        public int PlayerId { get; set; }

        [JsonIgnore]
        public Player? Player { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}

