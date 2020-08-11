using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Lista_Contato.Controllers
{
    public class ContatoController : Controller
    {

        private Models.ContatoEntities db = new Models.ContatoEntities();

        // GET: Contato
        public ActionResult Index()
        {
            List<Models.Contato> contatoList = null;
            if (ModelState.IsValid)
            {
                contatoList = db.Contato.ToList();
            }

            return View(contatoList);

        }

        public ActionResult Create()
        {
            Models.Contato contato = new Models.Contato();
            return View(new Models.Contato());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Contato contato, string tel, string mail)
        {
            if (string.IsNullOrWhiteSpace(contato.Nome))
            {
                ModelState.AddModelError("Nome", "Digite o nome desse Contato!");
            }
            if (ModelState.IsValid)
            {
                db.Contato.Add(contato);
                db.SaveChanges();
                int id = db.Contato.ToList().Last().Id;
                if (!string.IsNullOrWhiteSpace(tel))
                {
                    Models.Telefone telefone = new Models.Telefone()
                    {
                        IdContato = id,
                        Numero = tel
                    };
                    db.Telefone.Add(telefone);
                    db.SaveChanges();
                }
                if (!string.IsNullOrWhiteSpace(mail))
                {
                    Models.Email email = new Models.Email()
                    {
                        IdContato = id,
                        Email1 = mail
                    };
                    db.Email.Add(email);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(contato);
        }

        public ActionResult Details(int id = 0, string type = "")
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            ViewBag.type = type;
            Models.Contato contato = db.Contato.Find(id);
            return View(contato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Models.Contato contato)
        {
            if (string.IsNullOrWhiteSpace(contato.Nome))
            {
                ModelState.AddModelError("Nome", "Digite o nome desse Contato!");
            }
            if (ModelState.IsValid)
            {
                Models.Contato cdb = db.Contato.Find(contato.Id);
                cdb.Nome = contato.Nome;
                db.Entry(cdb).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = contato.Id, type = "Normal" });
            }
            ViewBag.type = "Edit";
            return View(contato);
        }

     

        public ActionResult AddTelefone(int id)
        {
            ViewBag.id = id;
            return View(new Models.Telefone());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTelefone(Models.Telefone telefone, int idContato)
        {
            if (string.IsNullOrWhiteSpace(telefone.Numero))
            {
                ModelState.AddModelError("Numero", "Digite um número!");
            }
            if (ModelState.IsValid)
            {
                telefone.IdContato = idContato;
                db.Telefone.Add(telefone);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = idContato, type = "Normal" });
            }
            ViewBag.id = idContato;
            return View(telefone);
        }

        public ActionResult AddEmail(int id)
        {
            ViewBag.id = id;
            return View(new Models.Email());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmail(Models.Email email, int idContato)
        {
            if (string.IsNullOrWhiteSpace(email.Email1))
            {
                ModelState.AddModelError("Email1", "Digite um e-mail!");
            }
            if (ModelState.IsValid)
            {
                email.IdContato = idContato;
                db.Email.Add(email);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = idContato, type = "Normal" });
            }
            ViewBag.id = idContato;
            return View(email);
        }

        public ActionResult EditNumber(int id)
        {
            var telefone = db.Telefone.Find(id);
            return View(telefone);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNumber(Models.Telefone telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone.Numero))
            {
                ModelState.AddModelError("Numero", "Digite um número!");
            }
            if (ModelState.IsValid)
            {
                var tel = db.Telefone.Find(telefone.Id);
                tel.Numero = telefone.Numero;
                db.Entry(tel).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = tel.IdContato, type = "Normal" });
            }
            return View(telefone);
        }

        public ActionResult EditEmail(int id)
        {
            var email = db.Email.Find(id);
            return View(email);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmail(Models.Email email)
        {
            if (string.IsNullOrWhiteSpace(email.Email1))
            {
                ModelState.AddModelError("Email1", "Digite um e-mail!");
            }
            if (ModelState.IsValid)
            {
                var mail = db.Email.Find(email.Id);
                mail.Email1 = email.Email1;
                db.Entry(mail).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = mail.IdContato, type = "Normal" });
            }
            return View(email);
        }

        public ActionResult DeleteContato(int id)
        {
            if (ModelState.IsValid)
            {

                db.Contato.Remove(db.Contato.Single(a => a.Id == id));
                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteNumber(int id)
        {
            int idContato = db.Telefone.Find(id).IdContato;
            if (ModelState.IsValid)
            {

                db.Telefone.Remove(db.Telefone.Single(a => a.Id == id));
                db.SaveChanges();

            }
            return RedirectToAction("Details", new { id = idContato, type = "Normal" });
        }

        public ActionResult DeleteEmail(int id)
        {
            int idContato = db.Email.Find(id).IdContato;
            if (ModelState.IsValid)
            {

                db.Email.Remove(db.Email.Single(a => a.Id == id));
                db.SaveChanges();

            }
            return RedirectToAction("Details", new { id = idContato, type = "Normal" });
        }
    }
}