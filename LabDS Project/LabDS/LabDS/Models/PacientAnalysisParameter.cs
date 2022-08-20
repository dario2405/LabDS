using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabDS.Models
{
    public class PacientAnalysisParameter
    {
        public int Id { get; set; }
        public int PacientAnalysisId { get; set; }
        public string Name { get; set; }
        public string Range { get; set; }
        public string Unit { get; set; }
        public double Value { get; set; }
        

    }
}