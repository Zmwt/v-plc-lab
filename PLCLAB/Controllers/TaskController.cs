using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Win32.TaskScheduler;
using PLCLAB.Models;

namespace PLCLAB.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        /*
        public ActionResult Index()
        {
            return View();
        }
        */


        [HttpPost]
        public ActionResult Set(FormCollection col)
        {
            int newTaskId;
            using (DBEntities PLCConsoleDB = new DBEntities())
            {
                newTaskId = PLCConsoleDB.Task.Count() + 1;
            }


            string almostrandom = DateTime.Now.Ticks.ToString(); // debug
            using (TaskService ts = new TaskService())
            {
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "PLC Task"; // opis taska

                TimeTrigger tTrig = new TimeTrigger(DateTime.Now);
                tTrig.StartBoundary = DateTime.Now + TimeSpan.FromMinutes(Convert.ToDouble(col["delay"]));
                tTrig.Repetition.Interval = TimeSpan.FromMinutes(Convert.ToDouble(col["interval"])); // minimum 1 minuta
                tTrig.EndBoundary = DateTime.Now + TimeSpan.FromMinutes(Convert.ToDouble(col["duration"])) + TimeSpan.FromMinutes(Convert.ToDouble(col["delay"]));
                td.Triggers.Add(tTrig);
                if (Convert.ToDouble(col["delay"]) == 0)
                {
                    td.Triggers.Add(new RegistrationTrigger { Delay = TimeSpan.FromMinutes(0) });
                }
                
                string str = col["id"] + " "
                    + col["ip"] + " "
                    + col["port"] + " "
                    + col["functionN"] + " "
                    + col["startingAddress"] + " "
                    + col["quantity"] + " "
                    + newTaskId
                ;

                td.Actions.Add(new ExecAction(Server.MapPath("~/App_Data/PLC_console.exe"), str));

                //ts.RootFolder.RegisterTaskDefinition("task " + almostrandom + "t" + col["startingAddress"] + "f" + col["functionN"] + "" + col["duration"] + "" + col["interval"], td);
                ts.RootFolder.RegisterTaskDefinition("task " + almostrandom, td);


            }





            return View();
        }

    }
}