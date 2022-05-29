using Trabalho.Grpc.Cliente.Models;

namespace Trabalho.Grpc.Cliente.Services
{
    public interface ICarrinhoGrpcService
    {
        Task<CarrinhoDTO> ObterCarrinho();
    }
}