using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawSystem.Models
{
    public class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Id { get; set; }
        public bool IsPresent { get; set; } = true;
    }
}