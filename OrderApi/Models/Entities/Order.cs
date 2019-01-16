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
        public long? Number { get; set; } //Could have named it OrderNumber. But since it is alraedy inside Order Class, Order prefix feels redundant
        public List<OrderProduct> Products { get; set; }
        public DateTime? Date { get; set; }
        public Customer CustomerDetails { get; set; }
    }
}
