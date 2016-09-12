using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vlabver01.Context;
using vlabver01.Models;
using vlabver01.ViewModels;

namespace vlabver01.Controllers
{
    public class PLCController : Controller
    {

        PLCContext db = new PLCContext();
        // GET: PLC
        public ActionResult Main()
        {
            return View(db.PLC.ToList());
        }



        public ActionResult Viewtest()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Viewtest([Bind(Include = "Name, Description, IPAddress, Port")]PLC plc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.PLC.Add(plc);
                    //db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Błąd zapisu bazy danych");
            }
            return View(plc);
        }



    }
}