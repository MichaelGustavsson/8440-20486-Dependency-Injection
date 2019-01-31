# Module 6
## Dependency Injection

In this updated module I have added dependency injection to correctly instantiate the repositories *CourseRepository* and *InstructorRepository*

To use Dependency Injection in ASP.NET MVC5 there are several very good libraries that can be used like Ninject and Unity.
I decided to use Unity because it is very easy to install and configure.

To use *Unity* just right click the project and select *Manage Nuget Packages* and in the *Nuget Package Manager* dialog search for **Unity** and **Unity.Mvc5** and install the packages.

When installed there should be a file *UnityConfig.cs* inside the folder **App_Start**
The code below shows the complete *RegisterComponents()* for the application.

```
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
```
In this file we configure Unity by register which Interface to map to which class. For example in our case if we want help to instantiate the CourseRepository class when we use the interface ICourseRepository we just add the following line of code inside the method RegisterComponents().

```
container.RegisterType<ICourseRepository, CourseRepository>();
```
That's all, so for each interface or abstract class we use we just need to register it and map it to correct class.
Because we are using **Microsoft Identity** to deal with **Authentication** and **Authorization** we also have to register the different Identity interface and abstract classes to concrete classes. That's why the three first lines are added to the method.

In the file **CoursesController.cs** I changed the instantiation of the repositories to use constructor dependency injection.

```
private ICourseRepository _courseRepo;
private IInstructorRepository _instructorRepo;

public CoursesController(ICourseRepository courseRepo, IInstructorRepository instructorRepo)
{
    _courseRepo = courseRepo;
    _instructorRepo = instructorRepo;
}
```
