using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Models
{
    public class RegisterModel
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Confirm Password"), Compare("Password", ErrorMessage = "Both Password doesn't match")]
        public string ConfirmPassword { get; set; }

    }
}
