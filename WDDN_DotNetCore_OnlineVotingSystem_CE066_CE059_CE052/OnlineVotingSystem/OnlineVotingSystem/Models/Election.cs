using OnlineVotingSystem.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models
{
    public class Election
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Ending Date")]
        [CompareDate]
        public string EndDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Result Date")]
        [CompareDate("EndDate")]
        public string ResultDate { get; set; }
    }
}
