using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AMS_v1._3.Models;
using System.Data.Entity;

namespace AMS_v1._3.Controllers
{
    public class HomeController : Controller
    {
        AMSEntities db = new AMSEntities();

        // Asynchronous GET: Home
        public async Task<ActionResult> Index()
        {
            if (Session["username"] != null)
            {
                string pid = Session["username"].ToString();
                var students = await db.student.Where(x => x.parentid == pid).ToListAsync();
                var parent = await db.parent.FindAsync(pid);

                ViewBag.Balance = parent?.balance ?? 0;
                ViewBag.UserId = parent?.id ?? 0;

                return View(students);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add(int id)
        {
            var data = db.parent.Where(x => x.id == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult Add(int id, parent Parent)
        {
            string pid = Session["username"].ToString();
            var edit = db.parent.Find(pid);

            edit.balance += Parent.balance;
            Session["balance"] = edit.balance;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(student Student)
        {
            if (db.student.Any(x => x.username == Student.username))
            {
                ViewBag.Notification = "Username Already Exists!";
                return View();
            }
            else
            {
                string pid = Session["username"].ToString();
                Student.parentid = pid;
                db.student.Add(Student);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var student = db.student.Where(x => x.id == id).FirstOrDefault();
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(int id, student Student)
        {
            string pid = Session["username"].ToString();
            var students = db.student.Find(id);
            var parents = db.parent.Find(pid);
            if (students.id == id)
            {
                int parentBal = Convert.ToInt32(Session["balance"]);

                // Check if the deduction is valid
                if (Student.balance <= parents.balance)
                {
                    students.balance = students.balance + Student.balance;
                    parents.balance = parents.balance - Student.balance;
                    Session["balance"] = parents.balance;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Detail(int id)
        {
            var data = db.student.Where(x => x.id == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = db.student.Where(x => x.id == id).FirstOrDefault();
            db.student.Remove(data);
            db.SaveChanges();
            ViewBag.Message = "Record Deleted!";
            return RedirectToAction("Index");
        }
    }
}
