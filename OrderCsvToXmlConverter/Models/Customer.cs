using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class Customer
    {
        public Customer() {
            Number = new Random().Next();
            Name = Number.ToString();
        }
        public long Number { get; set; }
        public string Name { get; set; }
    }
}
