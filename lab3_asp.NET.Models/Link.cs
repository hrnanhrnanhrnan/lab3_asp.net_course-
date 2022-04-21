using System;
using System.Collections.Generic;
using System.Text;

namespace lab3_asp.NET.Models
{
    public class Link
    {
        public int LinkId { get; set; }
        public string Url { get; set; }
        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}
