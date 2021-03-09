using GraphQL.Types;

namespace graphql_training_2
{
    public class ItemType : ObjectGraphType<Item>
    {
        public ItemType()
        {
            Field(i => i.Id);
            Field(i => i.Tag);
            Field(i => i.Title);
            Field(i => i.Price);
        }
    }
}
