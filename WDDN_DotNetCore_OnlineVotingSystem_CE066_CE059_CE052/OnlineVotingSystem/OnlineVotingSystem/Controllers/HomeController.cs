﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IResultRepository resultRepository;

        public HomeController(IElectionRepository electionRepository, IWebHostEnvironment hostingEnvironment,ICandidateRepository candidateRepository, IVoterRepository voterRepository, IResultRepository resultRepository)
        {
            this.electionRepository = electionRepository;
            this._hostingEnvironment = hostingEnvironment;
            this.candidateRepository = candidateRepository;
            this.voterRepository = voterRepository;
            this.resultRepository = resultRepository;
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
                Result resultData = new Result
                {
                    EndDate = model.EndDate,
                    ElectionName = model.Title,
                    WinnerName = "Winner Not Declared Yet",
                    Votes = 0
                };
                resultRepository.AddElection(resultData);
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
                    SignPath = uniqueSignName,
                    MobileNo = model.MobileNo,
                    VoterId = model.VoterId
                };
                voterRepository.ADD(voter);
                ViewData["message"] = "Voter Added SuccessFully...";
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Voting()
        {
            var model = candidateRepository.GetAllCandidate();
            List<SelectListItem> c = new List<SelectListItem>();
            foreach(var can in model)
            {
                c.Add(new SelectListItem { Text=can.Name , Value=can.Id+""});
            }
            var mymodel = new MyViewModel { Candidates = c };
            return View(mymodel);
        }

        [HttpPost]
        public IActionResult Voting(MyViewModel model)
        {
            ViewData["message"] = "";
            //var vid = HttpContext.Session.GetString("VoterId");
            var vid = 100;
            Voter v = voterRepository.GetVoter(vid);
            if (!v.HasVoted)
            {
                v.HasVoted = true;
                voterRepository.UPDATE(v);
                Candidate c = candidateRepository.GetCandidate(model.CID);
                c.Votes = c.Votes + 1;
                candidateRepository.UpdateCandidate(c);
                ViewData["message"] = "Voted SuccessFully...";
            }
            else
            {
                ViewData["message"] = "Already Voted...";
            }
            
            return View(model);
        }

        [HttpGet]
        public ViewResult List()
        {
            IEnumerable<Candidate> candidates =  candidateRepository.GetAllCandidate();
            return View(candidates);
        }

        [HttpPost,ActionName("List")]
        public IActionResult DeclareResult()
        {
            ViewData["message"] = "";
            var currentDate = DateTime.Now;
            List<Election> elections =  electionRepository.GetAllElection().ToList();
            foreach(var election in elections)
            {
                var endingDate = Convert.ToDateTime(election.EndDate);
                var resultDate = Convert.ToDateTime(election.ResultDate);
                if(currentDate < endingDate)
                {
                    ViewData["message"] = "Election has not ended yet";
                }
                else if(currentDate < resultDate)
                {
                    ViewData["message"] = $"Wait till {resultDate}";
                }
                else
                {
                    var result = resultRepository.GetResultBasedOnElectionEndDate(election.EndDate);
                    var winner = candidateRepository.GetCandidateWithHighestVotes();
                    result.EndDate = election.EndDate;
                    result.ElectionName = election.Title;
                    result.WinnerName = winner.Name;
                    result.Votes = winner.Votes;
                    
                    resultRepository.UpdateResult(result);
                    
                    ViewData["message"] = $"Result has declared successfully...";
                }
                break;
            }
            return View(candidateRepository.GetAllCandidate());
        }
        [HttpGet]
        public ViewResult Result()
        {
            var model = resultRepository.GetAllElectionResult();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
