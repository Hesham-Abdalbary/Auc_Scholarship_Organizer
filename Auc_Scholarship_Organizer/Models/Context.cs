using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Auc_Scholarship_Organizer.Models
{
    public class Context : DbContext
    {
        public Context() : base("DB")
        {

        }
 
        public DbSet<StudentApplication> StudentApplication { get; set; }
        public DbSet<User> User { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<Context>(null);
            base.OnModelCreating(modelBuilder);
        }

    }
}
