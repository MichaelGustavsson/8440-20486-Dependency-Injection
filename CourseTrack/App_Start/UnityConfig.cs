using CourseTrack.Models;
using CourseTrack.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace CourseTrack
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            //This is for register all of the Identity stuff with Unity.
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<DbContext, ApplicationDbContext>();
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            //These lines are added for each controller that uses depency injection.
            container.RegisterType<ICourseRepository, CourseRepository>();
            container.RegisterType<IInstructorRepository, InstructorRepository>();            

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}