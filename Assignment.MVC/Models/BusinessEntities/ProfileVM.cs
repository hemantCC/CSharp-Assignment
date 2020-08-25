using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment.MVC.Models.BusinessEntities
{
    public class ProfileVM
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Name is Required!")]
        public string Name { get; set; }

        [Phone]
        [Required(ErrorMessage = "Contact is Required!")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Address is Required!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is Required!")]
        [MaxLength(20, ErrorMessage = "Not More than 20 characters are allowed")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is Required!")]
        [MaxLength(20, ErrorMessage = "Not More than 20 characters are allowed")]
        public string State { get; set; }


        [Required(ErrorMessage = "Country is Required!")]
        [MaxLength(20, ErrorMessage = "Not More than 20 characters are allowed")]
        public string Country { get; set; }

        [Required(ErrorMessage = "ZipCode is Required!")]
        [MaxLength(6, ErrorMessage = "Not More than 6 characters are allowed")]
        public string ZipCode { get; set; }
    }
}