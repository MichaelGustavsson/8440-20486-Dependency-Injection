using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseTrack.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required()]
        [StringLength(4)]
        public string CourseNumber { get; set; }

        [Required()]
        [StringLength(128)]
        public string Title { get; set; }

        [Required()]
        [StringLength(512)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required()]
        [Range(2,5)]
        public int Days { get; set; }

        [Required()]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public int InstructorId { get; set; }

        public virtual Instructor Instructor { get; set; }
    }
}