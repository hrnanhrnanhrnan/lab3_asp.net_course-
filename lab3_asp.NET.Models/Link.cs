using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lab3_asp.NET.Models
{
    public class Link
    {
        [Key]
        public int LinkId { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}
