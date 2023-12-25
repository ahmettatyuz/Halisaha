using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace Halisaha.Entities
{
	public class ReservedSession
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public int SessionId { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public Session? Session { get; set; }

        [Required]
        public int EvSahibiTakimId { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public Team? EvSahibiTakim { get; set; }

        public int DeplasmanTakimId {get;set;}
        public Team? DeplasmanTakim {get;set;}

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}

