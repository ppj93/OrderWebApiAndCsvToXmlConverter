using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class Order
    {
        public Order() {
            Number = new Random().Next();
            LineNumber = 1;
            Products = new List<Product>();
            Date = DateTime.Now;
            CustomerDetails = new Customer();
        }
        public long Number { get; set; } //Could have named it OrderNumber. But since it is alraedy inside Order Class, Order prefix feels redundant
        public int LineNumber { get; set; }
        public List<Product> Products { get; set; }
        public DateTime? Date { get; set; }
        public Customer CustomerDetails { get; set; }
    }
}
