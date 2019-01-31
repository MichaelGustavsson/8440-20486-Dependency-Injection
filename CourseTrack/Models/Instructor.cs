using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseTrack.Models
{
    public class Instructor
    {
        public int InstructorId { get; set; }

        [Required()]
        [StringLength(80)]
        public string Name { get; set; }

        [Required()]
        [StringLength(128)]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}