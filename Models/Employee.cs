using Frontend.CustomValidation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public enum Gender { Male , Female }
    public enum Role { Manager , Developer , Designer }
    public class Employee
    {
        public int? id { get; set; }
        [RegularExpression(@"^[A-Za-z][A-Za-z\'\- ]{2,}$", ErrorMessage = "A name consist of letters only and at least 3 letters")]
        [DisplayName("Name")]
        public string name { get; set; }
        [EnumDataType(typeof(Role), ErrorMessage = "Role can only be Manager, Developer or Designer")]
        [DisplayName("Job Role")]
        public Role role { get; set; }
        [EnumDataType(typeof(Gender), ErrorMessage = "Gender can only be Male or Female")]
        [DisplayName("Gender")]
        public Gender gender { get; set; }
        [DisplayName("First Appointment ?")]
        public bool is1stAppointment { get; set; }
        [DateIsFutureVA]
        [DisplayName("Start Date")]
        public DateOnly startDate { get; set; }
        [DisplayName("Notes")]
        public string? notes { get; set; }
    }
}
