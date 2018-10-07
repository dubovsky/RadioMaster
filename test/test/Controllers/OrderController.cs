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
using PagedList;
namespace test.Controllers
{
    public class OrderController : Controller
    {


        
        private AdministerContext db = new AdministerContext();

        // GET: Order
        [Authorize(Roles = "user")]
        public async Task<ActionResult> Index()
        {

             var заказы = db.Заказы.Include(з => з.ВидРемонта).Include(з => з.Гарантии).Include(з => з.Запчасти).Include(з => з.Исполнители).Include(з => з.Клиенты).Include(з => з.СостояниеРемонта);
           
            return View(await заказы.ToListAsync());
        }

        // GET: Запчпсти на складе
        
        public ViewResult PartsInStore(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            //Сортировка по названию запчасти
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            

            //алгоритм пагинации
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var orders = from s in db.Запчасти
                         select s;
            //окно поиска "поиск по имени"
            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.НазваниеЗапчасти.Contains(searchString));
            }

            //фильтр по названию
            switch (sortOrder)
            {
                case "name_desc":
                    orders = orders.OrderByDescending(s => s.НазваниеЗапчасти);
                    break;
                default:
                    orders = orders.OrderBy(s => s.НазваниеЗапчасти);
                    break;
            }
            //количество строк на странице
            int pageSize = 4;
            //номер страницы
            int pageNumber = (page ?? 1);
            return View(orders.ToPagedList(pageNumber, pageSize));
           
        }

        //Get: Заказ детали
        [HttpGet]
        public ActionResult OrderPart()
        {
           // return View(await db.ЗаказДеталей.ToListAsync());
           // ViewBag.НоваяДетальID = new SelectList(db.ЗаказДеталей, "НоваяДетальID", "НоваяДетальID");
            //ViewBag.НазваниеДетали = new SelectList(db.ЗаказДеталей, "НазваниеДетали", "НазваниеДетали");
            //ViewBag.Количество = new SelectList(db.ЗаказДеталей, "Количество", "Количество");
            //ViewBag.ЗаводID = new SelectList(db.ЗаводИзготовитель, "ЗаводID", "ЗаводID");
            //ViewBag.Дата = new SelectList(db.ЗаказДеталей, "Дата", "Дата");

            ViewBag.НазваниеЗавода = new SelectList(db.ЗаводИзготовитель, "НазваниеЗавода", "НазваниеЗавода");

            return View();

        }
        //Post: Заказ детали
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OrderPart([Bind(Include = "НазваниеДетали,Дата,Количество")] ЗаказДеталей детали,
             string FactoryName)
        {
            int id = 0;
            foreach (var item in db.ЗаводИзготовитель)
            {
                if(item.НазваниеЗавода==FactoryName)
                {
                    id = item.ЗаводID;
                    break;
                }
            }
            int i = 0;
            foreach (var item in db.ЗаказДеталей)
            {
                if (item.НоваяДетальID>i)
                {
                    i = item.НоваяДетальID;
                }
            }

            детали.НоваяДетальID = i+1;
            детали.ЗаводID = id;
            

            if (ModelState.IsValid)
            {

                db.ЗаказДеталей.Add(детали);
                await db.SaveChangesAsync();
                ViewBag.Message = "Заявка на деталь отправлена";
                return RedirectToAction("Index");
            }
            return View(детали);

        }


        // GET: Order/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Заказы заказы = await db.Заказы.FindAsync(id);
            if (заказы == null)
            {
                return HttpNotFound();
            }
            return View(заказы);
        }

        //Get: Add client
        [HttpGet]
        public  ActionResult AddClient()
        {
            ViewBag.Телефон = new SelectList(db.Клиенты, "Телефон", "Телефон");
            ViewBag.Адрес = new SelectList(db.Клиенты, "Адрес", "Адрес");
            ViewBag.ФИО = new SelectList(db.Клиенты, "ФИО", "ФИО");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddClient([Bind(Include = "Телефон,Адрес,ФИО")] Клиенты клиент)
        {
            if (ModelState.IsValid)
            {
                db.Клиенты.Add(клиент);
                await db.SaveChangesAsync();
                return RedirectToAction("Create");
            }
            ViewBag.Телефон = new SelectList(db.Клиенты, "Телефон", "Телефон",клиент.Телефон);
            ViewBag.Адрес = new SelectList(db.Клиенты, "Адрес", "Адрес",клиент.Адрес);
            ViewBag.ФИО = new SelectList(db.Клиенты, "ФИО", "ФИО",клиент.ФИО);
            return View(клиент);
        }


        // GET: Order/Create
        public ActionResult Create()
        {
            ViewBag.ВидРемонтаID = new SelectList(db.ВидРемонта, "ВидРемонтаID", "ТипРемонта");
            ViewBag.ГарантияID = new SelectList(db.Гарантии, "ГарантияID", "Гарантия");
            ViewBag.ЗапчастьID = new SelectList(db.Запчасти, "ЗапчастьID", "НазваниеЗапчасти");
            ViewBag.masterName = new SelectList(db.Исполнители, "ФИО", "ФИО");
            ViewBag.clientName = new SelectList(db.Клиенты, "ФИО", "ФИО");
            ViewBag.СостояниеID = new SelectList(db.СостояниеРемонта, "СостояниеID", "Состояние");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ЗаказID,ДатаЗаказа,Поломка,ИсполнительID,СостояниеID,ГарантияID,КлиентID,ВидРемонтаID,ЗапчастьID")] Заказы заказы, 
            string clientName, string masterName)
        {
            
            //Переводим необходимые данные из string в int для их ID
            //для клиента
            foreach (var item in db.Клиенты)
            {
                if (item.ФИО==clientName)
                {
                    заказы.КлиентID = item.КлиентID;
                    break;
                }
            }
            //для исполнителя
            foreach (var item in db.Исполнители)
            {
                if (item.ФИО == masterName)
                {
                    заказы.ИсполнительID = item.ИсполнительID;
                    break;
                }
            }
            if (ModelState.IsValid)
            {
                db.Заказы.Add(заказы);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ВидРемонтаID = new SelectList(db.ВидРемонта, "ВидРемонтаID", "ТипРемонта");
            ViewBag.ГарантияID = new SelectList(db.Гарантии, "ГарантияID", "Гарантия");
            ViewBag.ЗапчастьID = new SelectList(db.Запчасти, "ЗапчастьID", "НазваниеЗапчасти");
            ViewBag.masterName = new SelectList(db.Исполнители, "ФИО", "ФИО");
            ViewBag.clientName = new SelectList(db.Клиенты, "ФИО", "ФИО");
            ViewBag.СостояниеID = new SelectList(db.СостояниеРемонта, "СостояниеID", "Состояние");
            return View(заказы);
        }

        // GET: Order/Edit/5
            public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Заказы заказы = await db.Заказы.FindAsync(id);
            if (заказы == null)
            {
                return HttpNotFound();
            }
           
            
            
            //ViewBag.Заказы = new List<Заказы>(db.Заказы);
            ViewBag.ВидРемонтаID = new SelectList(db.ВидРемонта, "ВидРемонтаID", "ТипРемонта", заказы.ВидРемонтаID);
            ViewBag.ГарантияID = new SelectList(db.Гарантии, "ГарантияID", "Гарантия", заказы.ГарантияID);
            ViewBag.ЗапчастьID = new SelectList(db.Запчасти, "ЗапчастьID", "НазваниеЗапчасти", заказы.ЗапчастьID);
            ViewBag.ИсполнительID = new SelectList(db.Исполнители, "ИсполнительID", "ФИО", заказы.Исполнители.ФИО);
            ViewBag.КлиентID = new SelectList(db.Клиенты, "КлиентID", "ФИО", заказы.Клиенты.ФИО);
           
            ViewBag.СостояниеID = new SelectList(db.СостояниеРемонта, "СостояниеID", "Состояние", заказы.СостояниеID);
            return View(заказы);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ЗаказID,ДатаЗаказа,Поломка,ИсполнительID,СостояниеID,ГарантияID,КлиентID,ВидРемонтаID,ЗапчастьID")] Заказы заказы)
        {
            if (ModelState.IsValid)
            {
                db.Entry(заказы).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ВидРемонтаID = new SelectList(db.ВидРемонта, "ВидРемонтаID", "ТипРемонта", заказы.ВидРемонтаID);
            ViewBag.ГарантияID = new SelectList(db.Гарантии, "ГарантияID", "Гарантия", заказы.ГарантияID);
            ViewBag.ЗапчастьID = new SelectList(db.Запчасти, "ЗапчастьID", "НазваниеЗапчасти", заказы.ЗапчастьID);
            ViewBag.ИсполнительID = new SelectList(db.Исполнители, "ИсполнительID", "Образование", заказы.ИсполнительID);
            ViewBag.КлиентID = new SelectList(db.Клиенты, "КлиентID", "Телефон", заказы.КлиентID);
            ViewBag.СостояниеID = new SelectList(db.СостояниеРемонта, "СостояниеID", "Состояние", заказы.СостояниеID);
            return View(заказы);
        }

        // GET: Order/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Заказы заказы = await db.Заказы.FindAsync(id);
            if (заказы == null)
            {
                return HttpNotFound();
            }
            return View(заказы);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Заказы заказы = await db.Заказы.FindAsync(id);
            db.Заказы.Remove(заказы);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        //Провеврка состояния заявки для клиента
        [HttpGet]
        public ActionResult State()
        {

            //var orders = await db.Заказы.ToListAsync();
            //return View(orders);
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> State(int? id)
        {
            if(id!=null)
            {
                Заказы order = await db.Заказы.FindAsync(id);
                return View(order);
            }
            

            return View();
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
