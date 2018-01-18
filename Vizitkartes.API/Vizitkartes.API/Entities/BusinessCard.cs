using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vizitkartes.API.Entities
{
    public class BusinessCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
		[ForeignKey("UserId")]
        public VizitkartesUser User { get; set; }
        public string UserId { get; set; }
    }
}
