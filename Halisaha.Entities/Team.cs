using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace Halisaha.Entities
{
	public class Team
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public int CaptainPlayer { get; set; }

		public List<Player> Players { get; set; }

        public List<ReservedSession>? ReservedSessions { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}

