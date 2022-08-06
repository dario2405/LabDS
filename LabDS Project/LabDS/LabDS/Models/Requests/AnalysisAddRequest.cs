using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabDS.Models.Requests
{
    public class AnalysisAddRequest : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public AnalysisCategory Category { get; set; }
        public double Price { get; set; }
        public string Range { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContex)
        {
            return new List<ValidationResult>();
        }
    }
}