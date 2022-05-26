using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Trabalho.Grpc.Servidor;

namespace Trabalho.Grpc.Cliente.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClienteController : Controller
    {
        public ClienteController()
        {
        }

        [HttpGet]
        public ActionResult ComunicacaoGrpc()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7117");
            return Ok();
        }
    }
}
