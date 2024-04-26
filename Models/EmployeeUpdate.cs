using Frontend.CustomValidation;
using Frontend.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class EmployeeUpdate
    {
        [Key]
        public int id { get; set; }
        
        [RegularExpression(@"^[A-Za-z][A-Za-z\'\- ]{2,}$", ErrorMessage = "A name consist of letters only and at least 3 letters")]
        [DisplayName("Name")]
        public string name { get; set; }
        [EnumDataType(typeof(Role), ErrorMessage = "Role can only be Manager (0), Developer (1) or Designer (2)")]
        [DisplayName("Job Role")]
        public Role role { get; set; }
        [EnumDataType(typeof(Gender), ErrorMessage = "Gender can only be Male (0) or Female (1)")]
        [DisplayName("Gender")]
        public Gender gender { get; set; }
        [DisplayName("First Appointment ?")]
        public bool is1stAppointment { get; set; }
        [DisplayName("Notes")]
        public string? notes { get; set; }
    }
}
