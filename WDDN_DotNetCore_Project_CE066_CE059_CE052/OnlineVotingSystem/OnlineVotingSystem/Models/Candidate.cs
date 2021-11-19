using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(18,Int64.MaxValue,ErrorMessage = "Age should be greater then 18...")]
        public string Age { get; set; }
        [Required]
        public string PhotoPath { get; set; }
        [Required]
        public string SignPath { get; set; }
        public int Votes { get; set; }
    }
}
