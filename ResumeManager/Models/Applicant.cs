﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResumeManager.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeManager.Models
{
    public class Applicant
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(150)]
        public string Name { get; set; } = "";

        [Required]
        [StringLength(10)]
        public string Gender { get; set; } = "";

        [NotMapped]
        public List<Gender> Genders { get; set; } = new List<Gender>();


        [Required]
        [Range(25, 55, ErrorMessage = "Currently,We Have no Positions Vacant for Your Age")]
        [DisplayName("Age in Years")]
        public int Age { get; set; }

        [Required]
        [StringLength(50)]
        public string Qualification { get; set; } = "";

        [NotMapped]
        //public List<Qualification> Qualifications { get; set; } = new List<Qualification>();
        public IEnumerable<SelectListItem> Qualifications { get; set; }

        [Required]
        [Range(1, 25, ErrorMessage = "Currently,We Have no Positions Vacant for Your Experience")]
        [DisplayName("Total Experience in Years")]
        public int TotalExperience { get; set; }

        public virtual List<Experience> Experiences { get; set; } = new List<Experience>();//detail very important

        public string ? PhotoUrl { get; set; }

        [Required(ErrorMessage = "Please choose the Profile Photo")]
        [Display(Name = "Profile Photo")]
        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }

        [StringLength(100)]
        public string Language { get; set; }

        [NotMapped]
        //public List<Qualification> Qualifications { get; set; } = new List<Qualification>();
        public List<Language> Languages { get; set; }

    }
}