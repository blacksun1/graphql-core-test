using GraphQL;
using GraphQL.Types;

namespace graphql_training_2
{
    public class GameStoreMutation : ObjectGraphType
    {
        private const string name = "item";

        public GameStoreMutation(IRepository repository)
        {
            FieldAsync<ItemType>(
                "createItem",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ItemInputType>>
                    {
                        Name = name
                    }
                ),
                resolve: async context =>
                {
                    var item = context.GetArgument<Item>(name);

                    return await repository.AddItem(item);
                }
            );

            FieldAsync<CustomerType>(
                "createCustomer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CustomerInputType>>
                    {
                        Name = "customer"
                    }
                ),
                resolve: async context =>
                {
                    var customer = context.GetArgument<Customer>("customer");

                    return await repository.AddCustomer(customer);
                }
            );

            FieldAsync<OrderType>(
                "createOrder",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<OrderInputType>>
                    {
                        Name = "order"
                    }
                ),
                resolve: async context =>
                {
                    var order = context.GetArgument<Order>("order");

                    return await repository.AddOrder(order);
                }
            );

            FieldAsync<OrderItemType>(
                "addOrderItem",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<OrderItemInputType>> { Name = "orderItem" }
                ),
                resolve: async ctx =>
                {
                    var orderItem = ctx.GetArgument<OrderItem>("orderItem");
                    return await repository.AddOrderItem(orderItem);
                });
        }
    }
}
