using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineVotingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.ViewModels
{
    public class MyViewModel
    {
        public int CID { get; set; }
        public List<SelectListItem> Candidates { get; set; }
    }
}
