﻿using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data
{
    public class ContactContext: DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options)
            :base(options)
        {
            
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
