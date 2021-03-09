using GraphQL.Types;

namespace graphql_training_2
{
    public class OrderItemType : ObjectGraphType<OrderItem>
    {
        public OrderItemType(IRepository repository)
        {
            Field(i => i.ItemId);

            FieldAsync<ItemType, Item>("item", resolve: ctx =>
            {
                return repository.GetItemById(ctx.Source.ItemId);
            });

            Field(i => i.Quantity);

            Field(i => i.OrderId);

            FieldAsync<OrderType, Order>("order", resolve: ctx =>
            {
                return repository.GetOrderById(ctx.Source.OrderId);
            });
        }
    }
}