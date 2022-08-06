using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabDS.Models.Requests
{
    public class LoginRequestModel
    {
        [Required]
        public string Username { get; set; }
        [Required, DataType(DataType.Password), MinLength(8)]
        public string Password { get; set; }
    }
}