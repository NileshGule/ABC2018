using System;
using System.ComponentModel.DataAnnotations;

namespace TechTalksAPI.Models
{
    public class TechTalk
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
    }
    
}