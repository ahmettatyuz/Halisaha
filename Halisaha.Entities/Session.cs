using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace Halisaha.Entities
{
    public class Session
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int OwnerId { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public Owner? Owner { get; set; } // navigation property

        [StringLength(50)]
        public string? SessionTime { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public List<ReservedSession>? ReservedSessions { get; set; }
    }
}

