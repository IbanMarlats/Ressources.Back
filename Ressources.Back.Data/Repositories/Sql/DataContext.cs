﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ressources.Back.Data.Models;
namespace Ressources.Back.Data.Repositories.Sql
{
    public class DataContext : DbContext
    {
        public DbSet<TypeUserModel> TypeUser { get; set; }
        public DbSet<UserModel> User { get; set; }
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
            Database.EnsureCreated();
            //Database.Migrate();
        }
    }
}
