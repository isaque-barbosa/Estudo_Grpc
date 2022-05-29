using Grpc.Core;
using Trabalho.Grpc.Servidor.Data;
using Trabalho.Grpc.Servidor.Models;
using Trabalho.Grpc.Servidor.Protos;

namespace Trabalho.Grpc.Servidor.Services
{
    //Se trabalhasse com OAuth2
    //[Authorize]
    public class CarrinhoGrpcService : CarrinhoCompras.CarrinhoComprasBase
    {
        private readonly ILogger<CarrinhoGrpcService> _logger;

        //Se tiver OAuth2 ou Context, a chamada seria feita aqui também
        private readonly CarrinhoContext _context = new();

        public CarrinhoGrpcService(ILogger<CarrinhoGrpcService> logger)
        {
            _logger = logger;
        }

        public override async Task<CarrinhoClienteResponse> ObterCarrinho(ObterCarrinhoRequest request, ServerCallContext context)
        {
            try
            {
                _logger.LogInformation("Chamando ObterCarrinho");

                var carrinho = await ObterCarrinhoCliente(Guid.Parse(request.ClienteId)) ?? new CarrinhoCliente();

                return MapCarrinhoClienteToProtoResponse(carrinho);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        private async Task<CarrinhoCliente> ObterCarrinhoCliente(Guid clienteId)
        {
            return _context.ObterCarrinho(clienteId);
        }

        private static CarrinhoClienteResponse MapCarrinhoClienteToProtoResponse(CarrinhoCliente carrinho)
        {
            var carrinhoProto = new CarrinhoClienteResponse
            {
                Id = carrinho.Id.ToString(),
                ClienteId = carrinho.ClienteId.ToString(),
                ValorTotal = (double)carrinho.ValorTotal,
                Desconto = (double)carrinho.Desconto,
                VoucherUtilizado = carrinho.VoucherUtilizado,
            };

            if(carrinho.Voucher is not null)
            {
                carrinhoProto.Voucher = new VoucherResponse
                {
                    Codigo = carrinho.Voucher.Codigo,
                    Percentual = (double?)carrinho.Voucher.Percentual ?? 0,
                    ValorDesconto = (double?)carrinho.Voucher.ValorDesconto ?? 0,
                    TipoDesconto = (int)carrinho.Voucher.TipoDesconto
                };
            }

            foreach(var item in carrinho.Itens)
            {
                carrinhoProto.Itens.Add(new CarrinhoItemResponse
                {
                    Id = item.Id.ToString(),
                    Nome = item.Nome,
                    Imagem = item.Imagem,
                    PeodutoId = item.ProdutoId.ToString(),
                    Quantidade = item.Quantidade,
                    Valor = (double)item.Valor
                });
            }

            return carrinhoProto;
        }
    }
}
