using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vlabver01.Context;
using vlabver01.Models;
using vlabver01.ViewModels;
using System.IO;
using EasyModbus;


namespace vlabver01.Controllers
{
    public class PLCController : Controller
    {

        PLCContext db = new PLCContext();
        public ActionResult Main()
        {
            return View(db.PLC.ToList());
        }


        public ActionResult AddPLC()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPLC([Bind(Include = "Name, Description, IPAddress, Port")]PLC plc, HttpPostedFileBase img)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (img != null && img.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(img.FileName);
                        var path = Path.Combine(Server.MapPath("~/App_Data/imgs"), fileName);
                        img.SaveAs(path);
                    }


                    db.PLC.Add(plc);
                    db.SaveChanges();

                   

                    return RedirectToAction("Main");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Błąd zapisu bazy danych");
            }
            return View(plc);
        }

        [HttpGet]
        [ActionName("DeletePLC")]
        public ActionResult DeletePLCGet(int id)
        {
            PLC plc = db.PLC.Find(id);
            
            return View(plc);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePLC(int id)
        {
            try
            {
                PLC plc = db.PLC.Find(id);
                db.PLC.Remove(plc);
                db.SaveChanges();
            }
            catch (DataException)
            {

            }
            return RedirectToAction("Main");
        }

        
        public ActionResult Connect(int id)
        {
            PLC plc = db.PLC.Find(id);


            return View(plc);
        }

    }
}