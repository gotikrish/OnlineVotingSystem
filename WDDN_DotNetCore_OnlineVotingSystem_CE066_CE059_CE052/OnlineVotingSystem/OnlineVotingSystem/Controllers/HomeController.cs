using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingSystem.Models;
using OnlineVotingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVotingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IElectionRepository electionRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ICandidateRepository candidateRepository;
        private readonly IVoterRepository voterRepository;

        public HomeController(IElectionRepository electionRepository, IWebHostEnvironment hostingEnvironment,ICandidateRepository candidateRepository, IVoterRepository voterRepository)
        {
            this.electionRepository = electionRepository;
            this._hostingEnvironment = hostingEnvironment;
            this.candidateRepository = candidateRepository;
            this.voterRepository = voterRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult CreateElection()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateElection(Election model)
        {
            ViewData["message"] = "";
            if (ModelState.IsValid)
            {
                ViewData["message"] = "Election SuccessFully Created";
                electionRepository.AddElection(model);
            }
            return View(model);
        }
        private string ProcessUploadFile(IFormFile photo)
        {
            string uniqueFileName = null;
            if (photo != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images","candidates");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        private string ProcessUploadFile1(IFormFile photo)
        {
            string uniqueFileName = null;
            if (photo != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images", "voters");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [HttpGet]
        public ViewResult AddCandidate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCandidate(CandidateViewModel model)
        {
            ViewData["message"] = "";
            if (ModelState.IsValid)
            {
                string uniquePhotoName = ProcessUploadFile(model.Photo);
                string uniqueSignName = ProcessUploadFile(model.Signature);
                Candidate candidate = new Candidate
                {
                    Name = model.Name,
                    Age = model.Age,
                    Votes = 0,
                    PhotoPath = uniquePhotoName,
                    SignPath = uniqueSignName
                };
                candidateRepository.AddCandidate(candidate);
                ViewData["message"] = "Candidate Added SuccessFully...";
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddVoter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddVoter(VoterViewModel model)
        {
            ViewData["message"] = "";
            if (ModelState.IsValid)
            {
                string uniquePhotoName = ProcessUploadFile1(model.Photo);
                string uniqueSignName = ProcessUploadFile1(model.Sign);
                Voter voter = new Voter
                {
                    Name = model.Name,
                    Age = model.Age,
                    HasVoted = false,
                    PhotoPath = uniquePhotoName,
                    SignPath = uniqueSignName
                };
                voterRepository.ADD(voter);
                ViewData["message"] = "Voter Added SuccessFully...";
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
