using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GraphQL.Linq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraphController : Controller
    {
        private readonly IDocumentExecuter _executer;
        private readonly IDocumentWriter _writer;
        private readonly ISchema _schema;
        private readonly GraphQLSettings _settings;
        private readonly IHostingEnvironment _env;

        public GraphController
        (
            GraphQLSettings settings,
            IHostingEnvironment env,
            IDocumentExecuter executer,
            IDocumentWriter writer,
            ISchema schema
        )
        {
            _settings = settings;
            _env = env;
            _executer = executer;
            _writer = writer;
            _schema = schema;
        }

        // GET: api/Graph
        [HttpGet]
        public ContentResult NoContentToShow()
        {
            return new ContentResult() {Content = "<div><b>GraphQL Server</b></div><p>Use the admin interface to interactively query the server</p>", ContentType = "text/html"};
        }

        // POST: api/Graph
        [HttpPost]
        [Produces("application/json")]
        public async Task<object> ExecuteQuery([FromBody]GraphQLRequest request)
        {
            var result = await _executer.ExecuteAsync
            (
                executionOptions =>
                {
                    executionOptions.Schema = _schema;
                    executionOptions.Query = request.Query;
                    executionOptions.OperationName = request.OperationName;
                    executionOptions.Inputs = request.Variables?.ToInputs();
                    executionOptions.UserContext = _settings.BuildUserContext?.Invoke(HttpContext);
                }
            );

            result.ExposeExceptions = _env.IsDevelopment();

            if (result.Errors?.FirstOrDefault() != null)
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.OK;
                return result.Errors;
            }

            return result;
        }
    }
}