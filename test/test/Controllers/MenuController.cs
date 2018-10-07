using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test.Models;
namespace test.Controllers
{
    public class MenuController : Controller
    {
         
            List<MenuItem> items = new List<MenuItem> 
            { 
             
            new MenuItem{Name="О нас", Controller="Home",  Action="About", Active=string.Empty},
            new MenuItem{Name="Контакты", Controller="Home",  Action="Contact", Active=string.Empty},
            new MenuItem{Name="Наши преимущества", Controller="Home",  Action="Previliges", Active=string.Empty},
            new MenuItem{Name="Домой", Controller="Home",  Action="Index", Active=string.Empty},
            new MenuItem{Name="Мастерам", Controller="Order",  Action="Index", Active=string.Empty},
            new MenuItem{Name="Администрирование", Controller="Admin",  Action="Index", Active=string.Empty}
           
            }; 

        // GET: Menu
        public PartialViewResult Main(string c)
        {
            var result = items.Where(m => m.Action == c);
            if (result.Any())
            {
                foreach (var item in result)
                {
                    if (item.Name=="Домой")
                    {
                        item.Active = "active";
                        break;
                    }
                    if (item.Name == "О нас")
                    {
                        item.Active = "active";
                        break;
                    }
                    if (item.Name == "Контакты")
                    {
                        item.Active = "active";
                        break;
                    }
                    if (item.Name == "Наши преимущества")
                    {
                        item.Active = "active";
                        break;
                    }
                    if (item.Name == "Мастерам")
                    {
                        item.Active = "active";
                        break;
                    }
                    if (item.Name == "Администрирование")
                    {
                        item.Active = "active";
                        break;
                    }
                    
                }
                
            }

            return PartialView(items);
        }

    }
}