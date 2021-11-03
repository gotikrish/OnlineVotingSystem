using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.ViewModels
{
    public class VoterViewModel
    {
        [Required]
        public int VoterId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Mobile Number must be of 10 digits...")]
        [Display(Name = "Mobile Number")]
        public string MobileNo { get; set; }
      
        public IFormFile Photo { get; set; }
 
        public IFormFile Sign { get; set; }
    }
}
