using System;
using System.Collections.Generic;
using System.Text;

namespace lab3_asp.NET.Models
{
    public class Interest
    {
        public int InterestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Link> Links { get; set; }
        public ICollection<PersonInterest> PersonInterests { get; set; }
    }
}
