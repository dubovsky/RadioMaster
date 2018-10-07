using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using test;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace test.Controllers
{
    public class AdminController : Controller
    {
        private AdministerContext db = new AdministerContext();

        // GET: Admin
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Исполнители.ToListAsync());
        }

        //GET: необходимые запчасти
        [HttpGet]
        public async Task<ActionResult> PartsToOrder()
        {
            return View(await db.ЗаказДеталей.ToListAsync());
        }
        //GET: поощрение сотрудников
        [HttpGet]
        public async Task<ActionResult> MasterStats()
        {
            ViewBag.Master = await db.Исполнители.ToListAsync();
            return View(await db.Заказы.ToListAsync());
        }
        //GET: вывод доходов
        [HttpGet]
        public async Task<ActionResult> Profit()
        {

            return View(await db.Заказы.ToListAsync());
        }
        //GET: вывод затрат на запчасти
        [HttpGet]
        public async Task<ActionResult> Investments()
        {

            return View(await db.Запчасти.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Исполнители исполнители = await db.Исполнители.FindAsync(id);
            if (исполнители == null)
            {
                return HttpNotFound();
            }
            return View(исполнители);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewBag.Qualification = new SelectList(db.КвалификацияМастера, "Квалификация", "Квалификация");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Стаж,Телефон,ФИО")] Исполнители исполнители, 
            string Qualification)
        {
            //присвоение id мастеру
            var masters = db.Исполнители.ToList();
            int id = masters.Count;
            id++;
            исполнители.ИсполнительID = id;
            // конец присвоения id

            //присвоение квалификации мастеру
            int qual = 0;
            foreach (var item in masters)
            {
                if (item.КвалификацияМастера.Квалификация==Qualification)
                {
                    qual = item.КвалификацияID;
                    break;
                }
            }
            if(qual==0)
            {
                return HttpNotFound();
            }
            исполнители.КвалификацияID = qual;
            //конец присвоения квалификации

            if (ModelState.IsValid)
            {
                db.Исполнители.Add(исполнители);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(исполнители);
        }

        // GET: Admin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.Quailification = new SelectList(db.КвалификацияМастера, "Квалификация", "Квалификация");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Исполнители исполнители = await db.Исполнители.FindAsync(id);
            if (исполнители == null)
            {
                return HttpNotFound();
            }
            return View(исполнители);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ИсполнительID,Стаж,Телефон,ФИО")] Исполнители исполнители,
            string Quailification)
        {
            int i = 0;
            foreach (Исполнители item in db.Исполнители)
            {
                if (item.КвалификацияМастера.Квалификация==Quailification)
                {
                    i = item.КвалификацияID;
                    break;
                }
            }
            if (i==0)
            {
                return HttpNotFound();
            }
            исполнители.КвалификацияID = i;

            if (ModelState.IsValid)
            {
                db.Entry(исполнители).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(исполнители);
        }

       
        [HttpPost]
        public void SaveToFile() //Сохраняем в файл данные о заявках за последний месяц
        {
            XDocument xdoc = new XDocument();
            XElement xroot = new XElement("Заявки");
            var rez = db.Заказы; //для удобства
            DateTime realDate = DateTime.Now; //для вывода списка заявок только за последний месяц

            foreach (var item in rez)
            {
                
                if (realDate.Month==item.ДатаЗаказа.Month)  //создаем список за последний месяц
                {
                    //Первый элемент
                    XElement elementId = new XElement("Заявка");
                    //Элементы этого элемента
                    XAttribute elAttr = new XAttribute("id", item.ЗаказID.ToString());
                    XElement elPartName; //если нет названия запчасти
                    if (item.ЗапчастьID == null)
                    {
                        elPartName = new XElement("НазваниеЗапчасти", "Запчасть отсутствует");
                    }
                    else
                    {
                        elPartName = new XElement("НазваниеЗапчасти", item.Запчасти.НазваниеЗапчасти.ToString());
                    }

                    XElement elRepairType = new XElement("ТипРемонта", item.ВидРемонта.ТипРемонта);
                    XElement elRepairState = new XElement("СостояниеРемонта", item.СостояниеРемонта.Состояние);
                    XElement elWarranty = new XElement("Гарантия", item.Гарантии.Гарантия);
                    XElement elClientPhone = new XElement("ТелефонКлиента", item.Клиенты.Телефон);
                    XElement elDate = new XElement("Датазаявки", item.ДатаЗаказа.ToShortDateString());
                    XElement elCrushType = new XElement("Поломка", item.Поломка);
                    //Конец элементов

                    //Добавляем все в элемент
                    elementId.Add(elAttr);
                    elementId.Add(elPartName);
                    elementId.Add(elRepairType);
                    elementId.Add(elRepairState);
                    elementId.Add(elWarranty);
                    elementId.Add(elClientPhone);
                    elementId.Add(elDate);
                    elementId.Add(elCrushType);
                    //Добалвяем в корень данные из таблицы Заказы
                    xroot.Add(elementId);
                }      
            }
            //Запись в документ
            xdoc.Add(xroot);
            //Сохраняем документ 
            Response.Clear(); //Optional: if we've sent anything before
            Response.ContentType = "application/xml"; //Must be 'text/xml'
            Response.ContentEncoding = System.Text.Encoding.UTF8; //We'd like UTF-8
            Response.AppendHeader("Content-Disposition", "attachment; filename=OrdersData.xml"); //без AppendHeader нет дилогового окна выбора
            //закачивания, а только выводится содержимое файла в окно браузера!

            //возвтрат ответа
            xdoc.Save(Response.Output);
            
            Response.End(); //Optional: will end processing
            
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ЗаказДеталей деталь = await db.ЗаказДеталей.FindAsync(id);
            if (деталь == null)
            {
                return HttpNotFound();
            }
            return View(деталь);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string price, int id) //метод удаления запчасти из списка на заказ на заводе
        {
            double pr=double.Parse(price);
            if (id==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ЗаказДеталей деталь = await db.ЗаказДеталей.FindAsync(id);
            int partsCount = db.Запчасти.Count();
            Запчасти part=new Запчасти();
            part.ЗапчастьID = partsCount + 1;
            part.НазваниеЗапчасти = деталь.НазваниеДетали;
            part.НаличиеID = 1;
            if(pr<0||pr==0)
            {
            return RedirectToAction("PartsToOrder");
            }
            part.ЦенаЗапчасти = pr; 
            part.ЗаводID = деталь.ЗаводID;
            //добавляем запчасть на склад
            db.Запчасти.Add(part);
            db.ЗаказДеталей.Remove(деталь);
            await db.SaveChangesAsync();
            return RedirectToAction("PartsToOrder");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
