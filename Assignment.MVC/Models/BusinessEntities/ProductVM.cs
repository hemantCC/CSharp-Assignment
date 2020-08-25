using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment.MVC.Models.BusinessEntities
{
    public class ProductVM
    {

        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        [Required(ErrorMessage = "Name is Required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Select Category!")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Code is Required!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Price is Required!")]
        public Nullable<int> Price { get; set; }

        public string Description { get; set; }
        [Required(ErrorMessage = "Status is Required!")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Discount is Required!")]
        public Nullable<int> Discount { get; set; }

        public string Image { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}