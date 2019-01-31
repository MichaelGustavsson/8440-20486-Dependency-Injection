using CourseTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseTrack.Repositories
{
    public interface IInstructorRepository
    {
        IList<Instructor> ListInstructors();
    }
}
