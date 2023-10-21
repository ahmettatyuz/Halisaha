using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Halisaha.Entities
{
    public class Session
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int OwnerId { get; set; }
        public Owner? Owner { get; set; } // navigation property

        [StringLength(50)]
        public string? SessionTime { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}

