using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using PLCLAB.Models;

[assembly: OwinStartupAttribute(typeof(PLCLAB.Startup))]
namespace PLCLAB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
            UserRoleSetup();
        }

        private void UserRoleSetup()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            if (!RoleManager.RoleExists("User"))
            {
                RoleManager.Create(new IdentityRole("User"));
            }

            if (!RoleManager.RoleExists("Admin"))
            {
                RoleManager.Create(new IdentityRole("Admin"));

                var admin = new ApplicationUser();
                admin.UserName = "Admin";
                admin.Email = "admin@admin.admin";

                var check = UserManager.Create(admin, "adminpass12");
                if (check.Succeeded)
                {
                    UserManager.AddToRole(admin.Id, "Admin");
                }

            }



        }
    }
}
