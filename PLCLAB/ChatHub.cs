using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
//using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace PLCLAB
{
    [HubName("Chat")]
    [Authorize]
    public class ChatHub : Hub
    {
        /*
        public void Hello()
        {
            Clients.All.hello();
        }
        */
        static int licz = 0;

        /*
        public void UserCount()
        {
            licz += 1;
            Clients.All.userCount(licz);
        }
        */


        public void Send(string msg)
        {
            var user = Context.User;
            string name = user.Identity.Name;
            if (msg.Length > 0)
            { 
                msg = name + " : " + msg;
                Clients.All.sendMsg(msg);
            }
        }

        public override Task OnConnected()
        {
            licz += 1;
            Clients.All.userCount(licz);
            return base.OnConnected();
        }
        public override Task OnReconnected()
        {
            licz += 1;
            Clients.All.userCount(licz);
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            licz -= 1;
            Clients.All.userCount(licz);
            return base.OnDisconnected(stopCalled);
        }

    }
}