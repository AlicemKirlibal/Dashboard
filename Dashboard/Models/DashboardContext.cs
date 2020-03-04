using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
 
namespace Dashboard.Models
{
    public class DashboardContext : DbContext
    {
        public DashboardContext() : base ("Tempo_MillenicomSalesDashboard")
        {
            Database.SetInitializer(new DashboardInitializer());
        }
        public DbSet<User> Users { get; set; }
    }
}