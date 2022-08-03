using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabDS.Models
{
    public enum RoleType : byte
    {
        NONE = 0,
        Admin = 1,
        User = 2,
        Pacient = 3
    }
    public enum GenderType : byte
    {
        Male = 1,
        Female = 2
    }
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int PhoneNO { get; set; }
        public RoleType Role { get; set; }
        public GenderType Gender { get; set; }
    }
}