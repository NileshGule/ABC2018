using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechTalksAPI.Models
{
    public class KeyValue
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
