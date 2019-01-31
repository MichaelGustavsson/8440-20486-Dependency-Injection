using CourseTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTrack.Repositories
{
    public interface ICourseRepository
    {
        IList<Course> ListCourses();
        Course FindCourse(int id);
        bool SaveCourse(Course course);
        bool DeleteCourse(int id);
        bool UpdateCourse(Course course);
    }
}
