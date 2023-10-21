using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Halisaha.Entities
{
	public class Message
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(500)]
        [Required]
        public string? MessageContent { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public int ReceiverId { get; set; }

        [Required]
        public int Status { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime Time { get; set; }

    }
}

