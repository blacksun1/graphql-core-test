using GraphQL.Types;

namespace graphql_training_2
{
    public class OrderItemInputType : InputObjectGraphType  
    {
        public OrderItemInputType()
        {
            Name = "OrderItemInput";
            Field<NonNullGraphType<IntGraphType>>("quantity");
            Field<NonNullGraphType<IntGraphType>>("itemId");
            Field<NonNullGraphType<IntGraphType>>("orderId");
        }
    }
}