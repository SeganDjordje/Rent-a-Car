using Autentifikacija.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using RentalCars.Models;

[assembly: OwinStartupAttribute(typeof(Autentifikacija.Startup))]
namespace Autentifikacija
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            // Need to execute the method once the application starts
            CreateRolesAndUsers();
        }

        // Create role and users method
        private void CreateRolesAndUsers()
        {
            // ApplicationDbContext is a class of the databse, instancing it we can now do CRUDE operations with context  
            // and we have connection with the database
            ApplicationDbContext context = new ApplicationDbContext();

            // Manages roles and users in the application
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            if (!roleManager.RoleExists(RoleName.Admin)) // Checks if a role named Admin exist, if not we go in the If statement
            {
                // Create Role Admin
                var role = new IdentityRole(); // Creates a new instace of a role
                role.Name = RoleName.Admin; // Now we can access the Name of a role and assign it to the RoleName.Admin
                roleManager.Create(role); // Creates a new role

                // Create an Administrator
                var user = new ApplicationUser(); // Creates a new instance of a user
                user.UserName = "DjordjeSegan@gmail.com"; // Now we can access the UserName of a user
                user.Email = "DjordjeSegan@gmail.com"; // Now we can access the Email of a user
                string userPwd = "DjS2023!"; 
                var result = userManager.Create(user, userPwd); // Creates a new user

                if (result.Succeeded) // If creaton is successfull 
                {
                    userManager.AddToRole(user.Id, RoleName.Admin); // Assings the role to the user
                }
            }

            // Create Role Employee
            if (!roleManager.RoleExists(RoleName.Employee))
            {
                var role = new IdentityRole();
                role.Name = RoleName.Employee;
                roleManager.Create(role);
            } 

            // Create Role User
            if (!roleManager.RoleExists(RoleName.User))
            {
                var role = new IdentityRole();
                role.Name = RoleName.User;
                roleManager.Create(role);
            }
        }
    }
}
