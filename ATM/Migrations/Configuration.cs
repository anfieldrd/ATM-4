namespace ATM.Migrations
{
    using ATM.Models;
    using ATM.Services;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ATM.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "ATM.Models.ApplicationDbContext";
        }

        protected override void Seed(ATM.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(t => t.UserName == "admin@mvcatm.com"))
            {
                var user = new ApplicationUser { UserName = "admin@mvcatm.com", Email = "admin@mvcatm.com" };
                userManager.Create(user, "passW0rd!");

                var service = new CheckingAccountService(context);
                service.CreateCheckingAccount("admin","user",user.Id, 1000);

                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Admin" });
                context.SaveChanges();

                userManager.AddToRole(user.Id, "Admin");
            }

            //context.Transactions.Add(new Transaction { Amount = 75, CheckingAccountId = 11 });
            //context.Transactions.Add(new Transaction { Amount = -25, CheckingAccountId = 11 });
            //context.Transactions.Add(new Transaction { Amount = 100000, CheckingAccountId = 11 });
            //context.Transactions.Add(new Transaction { Amount = 19.99m, CheckingAccountId = 11 });
            //context.Transactions.Add(new Transaction { Amount = 64.40m, CheckingAccountId = 11 });
            //context.Transactions.Add(new Transaction { Amount = 100, CheckingAccountId = 11 });
            //context.Transactions.Add(new Transaction { Amount = -300, CheckingAccountId = 11 });
            //context.Transactions.Add(new Transaction { Amount = 255.75m, CheckingAccountId = 11 });
            //context.Transactions.Add(new Transaction { Amount = 198, CheckingAccountId = 11 });
            //context.Transactions.Add(new Transaction { Amount = 2, CheckingAccountId = 11 });
            //context.Transactions.Add(new Transaction { Amount = 50, CheckingAccountId = 11 });
            //context.Transactions.Add(new Transaction { Amount = -1.50m, CheckingAccountId = 11 });
            //context.Transactions.Add(new Transaction { Amount = 6100, CheckingAccountId = 11 });
            //context.Transactions.Add(new Transaction { Amount = 164.84m, CheckingAccountId = 11 });
            //context.Transactions.Add(new Transaction { Amount = .01m, CheckingAccountId = 11 });

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
