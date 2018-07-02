using System.Security.Claims;

namespace GraphQL.Linq
{
    public class GraphQLUserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}
