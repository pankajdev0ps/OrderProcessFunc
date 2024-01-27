using System;

namespace OrderProcess
{
    public class Order
    {
        public string custEmail { get; set; }
        public int prodID { get; set; }
        public int prodCost { get; set; }
        public DateTime orderdate { get; set; }
    }
}