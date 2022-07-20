using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using ResumeManager.Data;
using ResumeManager.Models;

using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ResumeManager.Controllers
{
    public class ResumeController : Controller
    {
        private readonly ResumeDbContext _context;
        private readonly OutDbContext _Ocontext;

        private readonly IWebHostEnvironment _webHost;




        public ResumeController(ResumeDbContext context, OutDbContext Ocontext, IWebHostEnvironment webHost)
        {
            _context = context;
            _Ocontext = Ocontext;
            _webHost = webHost;

        }
        public IActionResult Index()
        {
            List<Applicant> applicants;
            applicants = _context.Applicants.ToList();

            return View(applicants);
        }

        public IActionResult NewList()
        {
            List<Applicant> applicants;
            applicants = _context.Applicants.ToList();

            //for (int i = 0; i < applicants.Count; i++)
            //{
            //    applicants[i].Genders = _Ocontext.Genders.ToList();
            //}


            // ViewBag.Genders = new SelectList(new List<string>() { "Male", "Female", "Unspecified" });
            ViewBag.Qualifications = new SelectList(_context.Qualifications.ToList(), "Text", "Text");
            ViewBag.Genders = new List<string>() { "Male", "Female", "Unspecified" };

            return View(applicants);

        }

        [HttpGet]
        public IActionResult Create()
        {
            Applicant applicant = new Applicant();
            applicant.Genders = _Ocontext.Genders.ToList();
            applicant.Qualifications = new SelectList(_context.Qualifications.ToList(), "Text", "Text");
            //applicant.Qualifications = new SelectList(_context.Qualifications.ToList(), "Id", "Text");

            applicant.Languages = _context.Languages.ToList();

            applicant.Experiences.Add(new Experience() { ExperienceId = 1 });
            //applicant.Experiences.Add(new Experience() { ExperienceId = 2 });
            //applicant.Experiences.Add(new Experience() { ExperienceId = 3 });
            return View(applicant);
        }


        [HttpPost]
        public IActionResult Create(Applicant applicant)
        {

            string uniqueFileName = GetUploadedFileName(applicant);
            applicant.PhotoUrl = uniqueFileName;

            for (var i = 0; i < applicant.Languages.Count; i++)
            {
                if (applicant.Languages[i].IsSelected == true)
                {
                    applicant.Language += "," + applicant.Languages[i].Text;
                }
            }
            applicant.Language = applicant.Language.TrimStart(',');

            _context.Add(applicant);
            _context.SaveChanges();
            return RedirectToAction("index");

        }


        private string GetUploadedFileName(Applicant applicant)
        {
            string uniqueFileName = null;

            if (applicant.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + applicant.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    applicant.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public async Task<IActionResult> ProcessEditReturnPartial(Applicant applicant)
        {
            _context.Update(applicant);
            await _context.SaveChangesAsync();

            return PartialView("_applicantCard", applicant);
        }

        public async Task<IActionResult> ShowOneProductJSON(int id)
        {
            if (id == null || _context.Applicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants.FindAsync(id);
            applicant.Genders = _Ocontext.Genders.ToList();

            if (applicant == null)
            {
                return NotFound();
            }



            return Json(applicant);
        }

    }
}
