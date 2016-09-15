using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace BeastLords.Models
{
    public class chathub : Hub
    {
        private static int _userCount = 0;
        private string userID;
        //private string thename;

        public void SendMessage(string from, string message)
        {
            Clients.All.AddMessage(from, message);
        }

        public void Sendname(string thename)
        {
            using (ContactUsContext db = new ContactUsContext())
            {

                var addChatInfo = new ChatInfo
                {
                    //  name = HttpContext.Current.Session["chatname"].ToString(),
                    name = thename,
                    signalRChatID = Context.ConnectionId.ToString()

                };

                db.ChatData.Add(addChatInfo);
                db.SaveChanges();
            }
        }

        public void saveData()
        {

        }


        public override Task OnConnected()
        {
           
            _userCount++;
    
            //when connnect add user to DB and then query and then spit out the username back, this only spit back one at a time and not the whole thing
            //except for counting.

            //add data to DB now
 

            userID= Context.ConnectionId;
          
            var context = GlobalHost.ConnectionManager.GetHubContext<chathub>();
            
            context.Clients.All.online(_userCount);

           // context.Clients.All.getName(thename);

            //get name from client
        //    context.Clients.All.getName();

            //using (ContactUsContext db = new ContactUsContext())
            //{

            //    var addChatInfo = new ChatInfo
            //    {
            //        //  name = HttpContext.Current.Session["chatname"].ToString(),
            //        name = HttpContext.Current.User.Identity.Name,
            //        signalRChatID = Context.ConnectionId.ToString()

            //    };

            //    db.ChatData.Add(addChatInfo);
            //    db.SaveChanges();

            //}

           
            return base.OnConnected();
        }
        public override Task OnReconnected()
        {
            _userCount++;
            var context = GlobalHost.ConnectionManager.GetHubContext<chathub>();
            context.Clients.All.online(_userCount);

            return base.OnReconnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            //when disconnect, remove user from list and query DB again to get the list.

            _userCount--;
            var context = GlobalHost.ConnectionManager.GetHubContext<chathub>();
            context.Clients.All.online(_userCount);

            return base.OnDisconnected(stopCalled);
        }
    }
}