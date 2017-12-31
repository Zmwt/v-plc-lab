using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PLCLAB.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        /*
        public bool Seed(ApplicationDbContext context)
        {
            bool success = false;

            ApplicationRoleManager _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

            success = this.CreateRole(_roleManager, "Admin", "Global Access");
            if (!success == true) return success;

            success = this.CreateRole(_roleManager, "CanEdit", "Edit existing records");
            if (!success == true) return success;

            success = this.CreateRole(_roleManager, "User", "Restricted to business domain activity");
            if (!success) return success;

            // Create my debug (testing) objects here

            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            ApplicationUser user = new ApplicationUser();
            PasswordHasher passwordHasher = new PasswordHasher();

            user.UserName = "youremail@testemail.com";
            user.Email = "youremail@testemail.com";

            IdentityResult result = userManager.Create(user, "Pass@123");

            success = this.AddUserToRole(userManager, user.Id, "Admin");
            if (!success) return success;

            success = this.AddUserToRole(userManager, user.Id, "CanEdit");
            if (!success) return success;

            success = this.AddUserToRole(userManager, user.Id, "User");
            if (!success) return success;

            return success;
        }
        */

    }





}