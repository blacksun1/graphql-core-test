using System;
using GraphQL.Types;
using GraphQL.Utilities;

namespace graphql_training_2
{
    public class GameStoreSchema : Schema
    {
        public GameStoreSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<GameStoreQuery>();
            Mutation = serviceProvider.GetRequiredService<GameStoreMutation>();
        }
    }
}