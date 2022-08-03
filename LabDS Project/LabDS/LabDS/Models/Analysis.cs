using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabDS.Models
{
    public class Analysis
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int CategoryId { get; set; }
        public AnalysisCategory Category { get; set; }
        public double Price { get; set; }
        public double Value { get; set; }
        public string Range { get; set; }
    }
}