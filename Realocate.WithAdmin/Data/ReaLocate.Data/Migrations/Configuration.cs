namespace ReaLocate.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Linq;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<User>(new UserStore<User>(context));
            var roleManager = new RoleManager<ExtendedUserRole>(new RoleStore<ExtendedUserRole>(context));

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new ExtendedUserRole { Name = "Regular", Description="Regular" });
                roleManager.Create(new ExtendedUserRole { Name = "Admin", Description = "Admin" });
                roleManager.Create(new ExtendedUserRole { Name = "AgencyOwner",Description= "AgencyOwner" });
                roleManager.Create(new ExtendedUserRole { Name = "Broker", Description= "Broker" });
            }

            context.SaveChanges();

            var adminUser = userManager.Users.FirstOrDefault(x => x.Email == "admin@gmail.com");

            if (adminUser == null)
            {
                var admin = new User
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "Admin",
                    ProfilePicturePath= "~/UploadedFiles/ProfileImages/avatar-placeholder.jpg",
                };

                userManager.Create(admin, "123456q");
                userManager.AddToRole(admin.Id, "Admin");
            }

            context.SaveChanges();

            var roles = roleManager.Roles.ToList();
        }
    }
}
