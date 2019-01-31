using System;
using System.Web.Mvc;
using CourseTrack.Models;
using CourseTrack.Repositories;

namespace CourseTrack.Controllers
{
    public class CoursesController : Controller
    {
        private ICourseRepository _courseRepo;
        private IInstructorRepository _instructorRepo;

        public CoursesController(ICourseRepository courseRepo, IInstructorRepository instructorRepo)
        {
            _courseRepo = courseRepo;
            _instructorRepo = instructorRepo;
        }
        
        public ActionResult Index()
        {
            var courses = _courseRepo.ListCourses();
            return View(courses);
        }

        public ActionResult Details(int id)
        {            
            var course = _courseRepo.FindCourse(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.InstructorId = new SelectList(_instructorRepo.ListInstructors(), "InstructorId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CourseNumber,Title,Description,Days,ReleaseDate,InstructorId")] Course course)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(_courseRepo.SaveCourse(course))
                        return RedirectToAction("Index");

                    ViewBag.ErrorMessage = $"Couldn't save course {course.Title}";
                    return View("ErrorPage");
                }
                catch (Exception ex)
                {
                    // Do some logging and return an error page for the user.
                    ViewBag.ErrorMessage = ex.Message;
                    return View("ErrorPage");
                }
                
            }

            ViewBag.InstructorId = new SelectList(_instructorRepo.ListInstructors(), 
                "InstructorId", "Name", course.InstructorId);
            return View(course);
        }

        public ActionResult Edit(int id)
        {
            var course = _courseRepo.FindCourse(id);
            if (course == null){ return HttpNotFound(); }

            ViewBag.InstructorId = new SelectList(_instructorRepo.ListInstructors(), "InstructorId", "Name", course.InstructorId);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,CourseNumber,Title,Description,Days,ReleaseDate,InstructorId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _courseRepo.UpdateCourse(course);
                return RedirectToAction("Index");
            }
            ViewBag.InstructorId = new SelectList(_instructorRepo.ListInstructors(), "InstructorId", "Name", course.InstructorId);
            return View(course);
        }

        public ActionResult Delete(int id)
        {
            var course = _courseRepo.FindCourse(id);
            if (course == null) { return HttpNotFound(); }
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(_courseRepo.DeleteCourse(id))
                return RedirectToAction("Index");

            ViewBag.ErrorMessage = $"Couldn't remove course with id {id}";
            return View("ErrorPage");
        }
    }
}
