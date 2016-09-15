using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

using BeastLords.Models;
using System.Net;
using HtmlAgilityPack;
using Microsoft.AspNet.SignalR;
using System.Web.Security;


namespace BeastLords.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            
          
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Chat()
        {
            TempData.Keep();

            return View();
        }

  
        public ActionResult ContactNow()
        {
            ContactUs model = new ContactUs();
 
            return PartialView("_ContactUs",model);
        }


        public ActionResult Chatinfo()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Chatinfo(ChatInfo model)
        {
            if (ModelState.IsValid)
            {
           
                    var chatinfo = new ChatInfo()
                    {
                        name = model.name,

                    };
             
                    TempData["chatName"] = chatinfo.name;

                    FormsAuthentication.SetAuthCookie(chatinfo.name, true);

                    return RedirectToAction("Chat");

             

            }

            return View();
        }


        [HttpPost]
        public ActionResult ContactUs(ContactUs model) {

            if (ModelState.IsValid)
            {
               
              
                try
                {
                    //try to save to DB first;
                

                    if (SaveContactData(model))
                    {
                        //send email here
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp-server.cfl.rr.com";
                        smtp.Credentials = new NetworkCredential("hlam49@cfl.rr.com", "[password]");
                        smtp.Port = 587;

                        MailMessage mail = new MailMessage();
                        MailAddress from_email = new MailAddress("hlam49@cfl.rr.com");
                        mail.From = from_email;
                        mail.To.Add(new MailAddress("akira32810@gmail.com"));

                        mail.Subject = "Name: " + model.Name + ", Email: " + model.Email + ", " + model.Subject + " (Ark: Survival Evolved)";
                        mail.IsBodyHtml = true;

                        mail.Body = model.Message;


                        smtp.Send(mail);

                        mail.Dispose();

                        return Content("<p style='color:#C7F050;'>Mail sent sucessfully!</p>");
                    }

                    else
                    {
                        return Content("<p style='color:red;'>Error sending mail and save info to server!</p>");
                    }
                  

                }

                catch
                {
                    return Content("<p style='color:red;'>Error sending mail, please try again later</p>");
                }
            }

            else
            {
              
                return PartialView("_ContactUs", model);
              
   
            }

            
        }

        //save data to DB from contact US
        public bool SaveContactData(ContactUs model)
        {
            bool isDataSaved = false;
            
            try 
            {
                using (ContactUsContext db = new ContactUsContext())
                {
                    var contactInfo = new ContactUSDb
                    {
                        Email = model.Email,
                        Name = model.Name,
                        Subject = model.Subject,
                        Message = model.Subject,
                        DateCreated = DateTime.Now
                    };

                    db.ContactUsData.Add(contactInfo);
                    db.SaveChanges();

                }

                isDataSaved = true;
            }
            catch 
            {
              
                isDataSaved = false;
            }

            return isDataSaved;
        }

        //check to see if server is online and return number of players with htmlparse
        public ActionResult ReturnServerInfo()
        {
           
            bool isServerOnline = true;
            string numOfPlayers = string.Empty;
         
           
            HtmlWeb beastlordServerHTML = new HtmlWeb();

            HtmlDocument doc = beastlordServerHTML.Load("https://arkservers.net/1/search/?term=beastlords.com%20PVE%20server");
            //beastlordServerHTML.LoadHtml(new WebClient().DownloadString("https://arkservers.net/server/204.12.254.122:27015"));

            var node = doc.DocumentNode.SelectSingleNode("//span[@class='grav_srv_players']");

            if (node != null)
            {
               numOfPlayers += node.InnerText;
            }

            else
            {
                isServerOnline = false;
            }

            var result = new { online = isServerOnline, players = numOfPlayers};

            return Json(result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult LoadUpvote()
        {
            var url = "https://arkservers.net/server/204.12.254.122:27015";

            return JavaScript("window.location = '" + url + "'");
        }
        public ActionResult LoadHome()
        {
            return PartialView("_main");
        }

        public ActionResult LoadBlog()
        {
            return PartialView("_Blog");
        }

        public ActionResult LoadQA()
        {
            return PartialView("_QA");
        }

        public ActionResult LoadServerInfo()
        {
            return PartialView("_ServerInfo");
        }

        public ActionResult LoadDonate()
        {
            return PartialView("_Donate");
        }


    }
}