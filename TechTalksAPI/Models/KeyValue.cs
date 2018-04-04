using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

    public class TechTalksDBContext : DbContext
    {
        public TechTalksDBContext(DbContextOptions<TechTalksDBContext> options)
            : base(options)
        {
        }

        public DbSet<KeyValue> KeyValues { get; set; }
        public DbSet<TechTalk> TechTalks { get; set; }
    }
}
