using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using CourseTrack.Models;

namespace CourseTrack.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseTrackerContext _context;
        public CourseRepository()
        {
            _context = new CourseTrackerContext();
        }

        public bool DeleteCourse(int id)
        {
            try
            {
                var course = _context.Courses.Find(id);
                if (course == null) return false;

                _context.Courses.Remove(course);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                // Do some logging stuff here.
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public Course FindCourse(int id)
        {
            return _context.Courses.Find(id);
        }

        public IList<Course> ListCourses()
        {
            return _context.Courses.Include("Instructor").ToList();
        }

        public bool SaveCourse(Course course)
        {
            try
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                // Do some logging here.
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public bool UpdateCourse(Course course)
        {
            try
            {
                _context.Entry(course).State = EntityState.Modified;
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                // Do some logging here.
                Debug.WriteLine(e.Message);
                return false;
            }
        }
    }
}