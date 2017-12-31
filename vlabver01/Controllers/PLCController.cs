using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PLCLAB.Context;
using PLCLAB.Models;
using PLCLAB.ViewModels;
using System.IO;
using EasyModbus;


namespace PLCLAB.Controllers
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

                        var fileTypes = new[] { "png", "gif", "jpg", "jpeg", "bmp" };
                        var fileExt = System.IO.Path.GetExtension(fileName).Substring(1);
                        if (!fileTypes.Contains(fileExt))
                        {
                            ModelState.AddModelError("", "Przyjmowane są tylko pliki z rozszerzeniami : .png, .gif, .jpg, .jpeg, .bmp");
                            return View(plc);
                        }

                       
                        var path = Path.Combine(Server.MapPath("~/Content/plcimg/" + plc.Name + "/"),  fileName);
                        Directory.CreateDirectory(Server.MapPath("~/Content/plcimg/" + plc.Name));
                        img.SaveAs(path);
                        plc.ImgPath = path;
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
            
            if (plc.ImgPath != null)
            {
                try
                {
                    System.IO.File.Delete(plc.ImgPath);
                    Directory.Delete(Server.MapPath("~/Content/plcimg/" + plc.Name));
                }
                catch { }
            }

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


        // readcoils
        //[Route("PLC/Read/{id}")]
        [HttpPost]
        public ActionResult Read(int id, int startingAddress, int quantity, int functionN)
        {
            PLCReadViewModel vm = new PLCReadViewModel();
            vm.PLC = db.PLC.Find(id);
            //PLC plc = db.PLC.Find(id);
            vm.PLCRead = new PLCRead();

            
            //PLCRead plcRead = new PLCRead();
            vm.PLCRead.StartingAddress = startingAddress;
            vm.PLCRead.Quantity = quantity;
            vm.PLCRead.FunctionN = functionN;
            
            switch (vm.PLCRead.FunctionN)
            {
                case 1:
                    {
                        vm.PLCRead.Val = vm.PLC.ReadQ(startingAddress, quantity);
                        break;
                    }

                case 2:
                    {
                        vm.PLCRead.Val = vm.PLC.ReadI(startingAddress, quantity);
                        break;
                    }

                case 3:
                    {
                        vm.PLCRead.Val = vm.PLC.ReadR(startingAddress, quantity);
                        break;
                    }

                case 4:
                    {
                        vm.PLCRead.Val = vm.PLC.ReadAI(startingAddress, quantity);
                        break;
                    }


                default:
                    break;
            }

            //ViewBag.Read = plc.Read(startingAddress, quantity);
            //ViewBag.StartingAddress = startingAddress;
            //ViewBag.Quantity = quantity;
            //ViewBag.FunctionN = functionN;

            return View(vm);
        }

    }
}