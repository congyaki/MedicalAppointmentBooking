using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class DoctorBasicVM
    {
        public int Id { get; set; }
        public int Experience { get; set; }
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public string SpecializationName { get; set; }
    }
}
