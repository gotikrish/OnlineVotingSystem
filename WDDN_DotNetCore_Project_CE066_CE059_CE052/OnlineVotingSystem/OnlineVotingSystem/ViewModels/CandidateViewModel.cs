using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.ViewModels
{
    public class CandidateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(18, Int64.MaxValue, ErrorMessage = "Age should be greater then 18...")]
        public string Age { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public IFormFile Signature { get; set; }
    }
}
