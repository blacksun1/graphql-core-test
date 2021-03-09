using System.Collections.Generic;

namespace graphql_training_2
{
    public class Item
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
