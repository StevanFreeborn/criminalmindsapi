using Microsoft.AspNetCore.Mvc;

namespace server.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/episodes")]
    [Produces("application/json")]
    public class CharactersController : ControllerBase
    {
    }
}
