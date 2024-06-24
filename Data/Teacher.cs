using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace efCoreApp.Data
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public string? TeacherSurname { get; set; }

        public string TeacherFullName { get
        {
            return this.TeacherName + " " + this.TeacherSurname;
        }}
        public string? TeacherEmail { get; set; }
        public string? TeacherPhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime RegisterDate { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}