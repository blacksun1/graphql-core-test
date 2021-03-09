using System.Collections.Generic;
using GraphQL.Types;

namespace graphql_training_2
{
    public class CustomerType : ObjectGraphType<Customer>
    {
        public CustomerType(IRepository repository)
        {
            Field(c => c.Name);
            Field(c => c.BillingAddress);

            FieldAsync<ListGraphType<OrderType>, IReadOnlyCollection<Order>>(
                "items",
                resolve: ctx =>
                {
                    return repository.GetOrdersByCustomerId(ctx.Source.CustomerId);
                }
            );
        }
    }
}