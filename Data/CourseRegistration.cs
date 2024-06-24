using System.ComponentModel.DataAnnotations;

namespace efCoreApp.Data
{
    public class CourseRegister
    {
        [Key]

        [Display(Name = "Register Id")]
        public int RegisterId { get; set; }

        [Display(Name = "Student Name")]

        [Required(ErrorMessage = "The Student Name field is required.")]
        public int StudentId { get; set; }

        [Required(ErrorMessage ="The Course Name field is required.")]

        public Student Student { get; set;} = null!;
        public Course Course { get; set; } = null!;

        [Display(Name ="Course Name")]
        public int CourseId { get; set; }

        [Display(Name = "Register Date")]
        public DateTime RegisterDate{ get; set; }
    }
}