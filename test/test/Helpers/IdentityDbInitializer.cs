using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using test.Models;

namespace test.Helpers
{
    public class IdentityDbInitializer: CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //base.Seed(context);

            // Создаем менеджеры ролей и пользователей 
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            IdentityRole roleAdmin, roleUser = null;
            ApplicationUser userAdmin = null;

            // поиск роли admin 
            roleAdmin = roleManager.FindByName("admin");
            if (roleAdmin == null)
            {
                // создаем, если на нашли 
                roleAdmin = new IdentityRole { Name = "admin" };
                var result = roleManager.Create(roleAdmin);
            }

            // поиск роли user 
            roleUser = roleManager.FindByName("user");
            if (roleUser == null)
            {
                // создаем, если на нашли 
                roleUser = new IdentityRole { Name = "user" };
                var result = roleManager.Create(roleUser);
            }

            // поиск пользователя (админ) bondagefist@mail.ru 
            userAdmin = userManager.FindByEmail("bondagefist@mail.ru");
            if (userAdmin == null)
            {
                // создаем, если на нашли
                userAdmin = new ApplicationUser { Email = "bondagefist@mail.ru", UserName = "bondagefist@mail.ru" };
                userManager.Create(userAdmin, "123456");
                // добавляем к роли admin 
                userManager.AddToRole(userAdmin.Id, "admin");
            }

            // Создаем аккаунты для пяти мастеров
            ApplicationUser userMaster1 = null;
            // поиск пользователя radioMaster1@mail.ru
            userMaster1 = userManager.FindByEmail("radioMaster1@mail.ru");
            if (userMaster1 == null)
            {
                // создаем, если на нашли
                userMaster1 = new ApplicationUser { Email = "radioMaster1@mail.ru", UserName = "radioMaster1@mail.ru" };
                userManager.Create(userMaster1, "master1");
                // добавляем к роли user
                userManager.AddToRole(userMaster1.Id, "user");

            }

        }
    }
}