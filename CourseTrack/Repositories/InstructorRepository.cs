using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseTrack.Models;

namespace CourseTrack.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private CourseTrackerContext _context;

        public InstructorRepository()
        {
            _context = new CourseTrackerContext();
        }
        public IList<Instructor> ListInstructors()
        {
            return _context.Instructors.ToList();
        }
    }
}