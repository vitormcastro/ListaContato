using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}