using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechTalksAPI.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        // public virtual ICollection<TechTalk> TechTalks { get; set; }
    }
    
}