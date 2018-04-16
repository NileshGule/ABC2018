using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechTalksAPI.Models
{
    public class TechTalkDTO
    {
        
        public int Id { get; set; }
        public string TechTalkName { get; set; }
        // [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        
        // public virtual Categories Category { get; set;}
    }
    
}