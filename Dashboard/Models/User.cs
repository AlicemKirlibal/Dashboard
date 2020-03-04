using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    public class User
    {
        public int Id { get; set; }

        public string AgentName { get; set; }

        public int SalesCount { get; set; }

        public int TotalSales { get; set; }

        public DateTime SalesDate { get; set; }
        

    


    }
}