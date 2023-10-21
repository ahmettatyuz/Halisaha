using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Halisaha.Entities
{
	public class ReservedSession
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required]
        public DateTime Time { get; set; }

        //[Required]
        public int SessionId { get; set; }
        public Session? Session { get; set; }

        //[Required]
        public int PlayerId { get; set; }
        public Player? Player { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}

