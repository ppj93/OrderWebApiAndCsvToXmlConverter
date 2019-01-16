using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class XmlNodeNames
    {
        public static class Orders
        {
            public static readonly string Date = "Date";
            public static readonly string Order = "Order";
            public static readonly string Products = "Products";
            public static readonly string Number = "Number";
        }

        public static class Customer
        {
            public static readonly string Number = "Number";
            public static readonly string Title = "Customer";
            public static readonly string Name = "Name";
        }

        public static class Product
        {
            public static readonly string Title = "Product";
            public static readonly string Name = "Name";
            public static readonly string Price = "Price";
            public static readonly string Description = "Description";
            public static readonly string Group = "Group";
            public static readonly string Quantity = "Quantity";
            public static readonly string Number = "Number";
        }

    }
}
