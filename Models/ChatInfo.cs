using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BeastLords.Models
{
    public class ChatInfo
    {
        [Key]
        public int ID { get; set; }

        public string signalRChatID { get; set; }

        [Required]
        public string name { get; set; }


    }

}