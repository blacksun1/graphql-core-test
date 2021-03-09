using System.Collections.Generic;

namespace graphql_training_2
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string BillingAddress { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}