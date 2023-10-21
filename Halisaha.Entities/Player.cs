using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations;

namespace Halisaha.Entities;

public class Player
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    [StringLength(100)]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(100)]
    public string? Lastname { get; set; }

    [Required]
    [StringLength(100)]
    public string? Mail { get; set; }

    [Required]
    [StringLength(11)]
    public string? Phone { get; set; }

    [Required]
    [StringLength(100)]
    public string? City { get; set; }

    [Required]
    [StringLength(200)]
    public string? Address { get; set; }

    [Required]
    [StringLength(50)]
    public string? Position { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.Now;

    [SwaggerSchema(ReadOnly = true)]
    public List<ReservedSession>? ReservedSessions { get; set; }

}

