using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    //Subclassed the type because base Product class can then be re-used to display Product details on Product Browsing page.
    [Serializable]
    public class OrderProduct: Product
    {
        public int Quantity { get; set; }
    }
}
