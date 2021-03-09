using System.Collections.Generic;
using System.Text.Json.Serialization;
using GraphQL.SystemTextJson;

namespace graphql_training_2
{
    public class GraphQLRequest
    {
        public string Query { get; set; }
        public string OperationName { get; set; }

        [JsonConverter(typeof(ObjectDictionaryConverter))]
        public Dictionary<string, object> Variables
        {
            get; set;
        }
    }
}