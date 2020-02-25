using Hippra.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hippra.Models.FTDesign
{
    public class FTRegisterModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "National Provider Identifier Number")]
        public int NPIN { get; set; }

        [Required]
        [Display(Name = "Medical Specialty")]
        public int MedicalSpecialty { get; set; }

        [Required]
        [Display(Name = "American Board Certified")]
        public bool AmericanBoardCertified { get; set; }

        [Required]
        [Display(Name = "Residency Hospital")]
        public string ResidencyHospital { get; set; }

        [Required]
        [Display(Name = "Medical School Attended")]
        public string MedicalSchoolAttended { get; set; }

        [Required]
        [Display(Name = "Education/Degree")]
        public string EducationDegree { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Zipcode")]
        public string Zipcode { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Check")]
        public bool AgreedTerm { get; set; }
    }
}
