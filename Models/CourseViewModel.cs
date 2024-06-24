using System.ComponentModel.DataAnnotations;
using efCoreApp.Data;

namespace efCoreApp.Models
{
    public class CourseViewModel
    {
        [Key]
        [Display(Name = "Course Id")]
        public int CourseId { get; set; }

        [Display(Name = "Course Header")]
        [StringLength(50)]
        public string? CourseHeader { get; set; }

        [Required(ErrorMessage = "The Teacher field is required.")]
        public int TeacherId { get; set; }
        public ICollection<CourseRegister> CourseRegister { get; set; } = new List<CourseRegister>();
    }
}