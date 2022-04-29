using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lab3_asp.NET.Models
{
    public class Interest
    {
        [Key]
        public int InterestId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Link> Links { get; set; }
        public ICollection<PersonInterest> PersonInterests { get; set; }
    }
}
