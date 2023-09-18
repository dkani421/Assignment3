using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComputerStore
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ComputerID { get; set; }
        public int ComponentID { get; set; }
        public decimal ComponentPrice { get; set; }
    }
}