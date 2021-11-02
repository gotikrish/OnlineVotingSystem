using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models
{
    public class Voter
    {
        [Key]
        public int VoterId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        [StringLength(10,ErrorMessage = "Mobile Number must be of 10 digits...")]
        [Display(Name = "Mobile Number")]
        public string MobileNo { get; set; }
        [Required]
        [Display(Name = "Photo")]
        public string PhotoPath { get; set; }
        [Required]
        [Display(Name = "Signature")]
        public string SignPath { get; set; }
        public bool HasVoted { get; set; }
    }
}
