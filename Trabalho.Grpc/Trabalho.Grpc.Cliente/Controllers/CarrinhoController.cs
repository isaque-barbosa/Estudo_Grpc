using Microsoft.AspNetCore.Mvc;
using System.Net;
using Trabalho.Grpc.Cliente.Models;
using Trabalho.Grpc.Cliente.Services;

namespace Trabalho.Grpc.Cliente.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    //[Authorize]
    public class CarrinhoController : Controller
    {
        private readonly ILogger<CarrinhoController> _logger;
        private readonly ICarrinhoGrpcService _carrinhoGrpcService;
        public CarrinhoController(ICarrinhoGrpcService carrinhoGrpcService,
            ILogger<CarrinhoController> logger)
        {
            _carrinhoGrpcService = carrinhoGrpcService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(CarrinhoDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterCarrinho()
        {
            _logger.LogInformation("Começo da requisição para obter carrinho.");

            CarrinhoDTO carrinho = await _carrinhoGrpcService.ObterCarrinho();

            if(carrinho is null)
            {
                _logger.LogInformation("Termino da requisição -> NoContent: Carrinho nulo.");
                return NoContent();
            }

            _logger.LogInformation("Termino da requisição -> Ok: Carrinho preenchido com sucesso.");
            return Ok(carrinho);
        }
    }
}
