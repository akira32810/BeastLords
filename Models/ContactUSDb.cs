using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BeastLords.Models
{
    public class ContactUSDb
    {
        [Key]
        public int ID { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public DateTime DateCreated { get; set; }

    }

    public class ContactUsContext : DbContext
    {
        public DbSet<ContactUSDb> ContactUsData { get; set; }
        public DbSet<ChatInfo> ChatData { get; set; }
    }
}