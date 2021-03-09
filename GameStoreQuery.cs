using System.Collections.Generic;
using GraphQL;
using GraphQL.Types;

namespace graphql_training_2
{
    public class GameStoreQuery : ObjectGraphType
    {
        public GameStoreQuery(IRepository repository)
        {
            Field<StringGraphType>(
                name: "name",
                resolve: context => "Steam"
            );

            FieldAsync<ItemType>(
                "item",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "tag"
                    }
                ),
                resolve: async context =>
                {
                    var tag = context.GetArgument<string>("tag");
                    return await repository.GetItemByTag(tag);
                }
            );

            FieldAsync<ListGraphType<ItemType>, IReadOnlyCollection<Item>>(
                "items",
                resolve: async context =>
                {
                    return await repository.GetItems();
                }
            );

            FieldAsync<ListGraphType<OrderType>, IReadOnlyCollection<Order>>(
                "orders",
                resolve: ctx =>
                {
                    return repository.GetOrders();
                }
            );

            FieldAsync<ListGraphType<CustomerType>, IReadOnlyCollection<Customer>>(
                "customers",
                resolve: ctx =>
                {
                    return repository.GetCustomers();
                }
            );

            FieldAsync<ListGraphType<OrderItemType>, IReadOnlyCollection<OrderItem>>(
                "orderItem",
                resolve: ctx =>
                {
                    return repository.GetOrderItem();
                });
        }
    }
}
