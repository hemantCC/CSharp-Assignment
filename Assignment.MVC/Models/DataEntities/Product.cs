using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment.MVC.Models.DataEntities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Code { get; set; }
        public Nullable<int> Price { get; set; }

        public string Description { get; set; }
        public string Status { get; set; }
        
        public Nullable<int> Discount { get; set; }

        public string Image { get; set; }
    }
}