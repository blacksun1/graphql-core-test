using GraphQL.Types;

namespace graphql_training_2
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType(IRepository repository)
        {
            Field(o => o.Tag);
            Field(o => o.CreatedAt);

            FieldAsync<CustomerType, Customer>("customer",
                resolve: ctx =>
                {
                    return repository.GetCustomerById(ctx.Source.CustomerId);
                });
        }
    }
}