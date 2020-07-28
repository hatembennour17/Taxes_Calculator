using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessor
{
    public class Order
    {
        public string orderName { get; set; }
        public double rowPrice { get; set; }
        public string finalPrice { get; set; }
        public int quantity { get; set; }
        public bool taxable { get; set; }
        public double taxes { get; set; }
        public bool imported { get; set; }


    }
}
