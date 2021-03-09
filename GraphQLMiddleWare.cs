using GraphQL;
using GraphQL.Types;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace graphql_training_2
{
    public class GraphQLMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDocumentExecuter _executor;
        private readonly GraphQLOptions _options;
        private readonly IDocumentWriter _writer;

        public GraphQLMiddleware(
            RequestDelegate next,
            IDocumentWriter writer,
            IDocumentExecuter executor,
            IOptions<GraphQLOptions> options)
        {
            _next = next;
            _writer = writer;
            _executor = executor;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext httpContext, ISchema schema)
        {
            if (
                !httpContext.Request.Path.StartsWithSegments(_options.EndPoint) ||
                !string.Equals(httpContext.Request.Method, "POST", StringComparison.OrdinalIgnoreCase))
            {
                await _next(httpContext);
                return;
            }

            var request = await JsonSerializer.DeserializeAsync<GraphQLRequest>(
                httpContext.Request.Body,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            var result = await _executor.ExecuteAsync(doc =>
                {
                    doc.Schema = schema;
                    doc.Query = request.Query;
                    doc.Inputs = request.Variables.ToInputs();
                    doc.OperationName = request.OperationName;
                }).ConfigureAwait(false);

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = 200;

            await _writer.WriteAsync(httpContext.Response.Body, result);
        }
    }

    public static class GraphQLMiddlewareExtensions
    {
        public static IApplicationBuilder UseGraphQL(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GraphQLMiddleware>();
        }

        public static IServiceCollection AddGraphQL(this IServiceCollection services, Action<GraphQLOptions> action)
        {
            return services.Configure(action);
        }
    }
}
