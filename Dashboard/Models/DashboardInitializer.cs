using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    public class DashboardInitializer : DropCreateDatabaseIfModelChanges<DashboardContext>
    {
        protected override void Seed(DashboardContext context)
        {
            List<User> users = new List<User>()
            {
                new User(){ AgentName="alicem", SalesCount=6, SalesDate=DateTime.Now},
                new User(){ AgentName="mehmet", SalesCount=10, SalesDate=DateTime.Now},
                new User(){ AgentName="aslı", SalesCount=8, SalesDate=DateTime.Now},
                new User(){ AgentName="kerem", SalesCount=12, SalesDate=DateTime.Now},

            };

            foreach (var item in users)
            {
                context.Users.Add(item);
            }
            context.SaveChanges();
            base.Seed(context);
        }

    }
}