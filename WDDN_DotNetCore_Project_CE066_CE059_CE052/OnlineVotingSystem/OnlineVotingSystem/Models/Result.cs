using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models
{
    public class Result
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Election")]
        public string ElectionName { get; set; }
        [Display(Name = "Ending Date")]
        public string EndDate { get; set; }
        [Display(Name = "Winner")]
        public string WinnerName { get; set; }
        public int Votes { get; set; }
    }
}
