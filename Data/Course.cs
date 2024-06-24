using System.ComponentModel.DataAnnotations;

namespace efCoreApp.Data
{
    public class Course
    {
        [Key]
        [Display(Name = "Course Id")]
        public int CourseId { get; set; }

        [Display(Name = "Course")]
        [Required]
        public string? CourseHeader { get; set; }


        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
        public ICollection<CourseRegister> CourseRegister { get; set; } = new List<CourseRegister>();
    }
}