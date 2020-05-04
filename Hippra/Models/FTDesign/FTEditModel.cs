using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hippra.Models.FTDesign
{
    public class FTEditModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


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


        [Display(Name = "Residency Hospital")]
        public string ResidencyHospital { get; set; }


        [Display(Name = "Medical School Attended")]
        public string MedicalSchoolAttended { get; set; }


        [Display(Name = "Education/Degree")]
        public string EducationDegree { get; set; }


        [Display(Name = "Address")]
        public string Address { get; set; }


        [Display(Name = "Zipcode")]
        public string Zipcode { get; set; }


        [Display(Name = "State")]
        public string State { get; set; }


        [Display(Name = "City")]
        public string City { get; set; }


        [Display(Name = "Contact Number")]
        //[Phone]
        public string PhoneNumber { get; set; }

        [Display(Name = "Check")]
        public bool AgreedTerm { get; set; }
    }
}
