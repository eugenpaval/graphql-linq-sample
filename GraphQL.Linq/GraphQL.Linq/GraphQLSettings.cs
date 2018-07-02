using System;
using Microsoft.AspNetCore.Http;

namespace GraphQL.Linq
{
    public class GraphQLSettings
    {
        public PathString Path { get; set; } = "/api";
        public Func<HttpContext, object> BuildUserContext { get; set; }
    }
}