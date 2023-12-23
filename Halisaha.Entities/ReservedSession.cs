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
        public int TeamId { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public Team? Team { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}

