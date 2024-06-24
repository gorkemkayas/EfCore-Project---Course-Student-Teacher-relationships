using System.ComponentModel.DataAnnotations;

namespace efCoreApp.Data
{
    public class Student{

        // primary key

        [Key]
        [Display(Name = "Student Id")]
        public int StudentId { get; set; }

        [Required]
        [Display(Name = "Student Name")]
        public string? StudentName { get; set; }

        [Required]
        [Display(Name ="Student Surname")]
        public string? StudentSurname { get; set; }

        public string StudentFullName { get
        {
            return this.StudentName + " " + this.StudentSurname;
        }}
        [Required]
        [Display(Name ="Student Email")]
        public string? StudentEmail { get; set; }
        [Required]
        [Display(Name ="Student Phone Number")]
        public string? StudentPhoneNumber { get; set; }

        public ICollection<CourseRegister> CourseRegister { get; set; } = new List<CourseRegister>();
    }
}