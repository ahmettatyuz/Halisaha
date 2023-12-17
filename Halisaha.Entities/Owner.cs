using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace Halisaha.Entities
{
	public class Owner
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Password { get; set; }

        [StringLength(100)]
        [Required]
        public string? PitchName { get; set; }

        [StringLength(100)]
        [Required]
        public string? OwnerFirstName { get; set; }

        [StringLength(100)]
        [Required]
        public string? OwnerLastName { get; set; }
        
        [StringLength(100)]
        [Required]
        public string? Mail { get; set; }

        [StringLength(100)]
        [Required]
        public string? Web { get; set; }

        [StringLength(11)]
        [Required]
        public string? Phone { get; set; }

        [StringLength(100)]
        [Required]
        public string? City { get; set; }

        [StringLength(200)]
        [Required]
        public string? Address { get; set; }

        [Required]
        public double Point { get; set; }

        [StringLength(100)]
        [Required]
        public string? Coordinate1 { get; set; }

        [StringLength(100)]
        [Required]
        public string? Coordinate2 { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        [SwaggerSchema(ReadOnly = true)]
        public List<Session>? Sessions { get; set; }
    }
}

