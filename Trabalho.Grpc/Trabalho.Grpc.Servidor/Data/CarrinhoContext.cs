using Trabalho.Grpc.Servidor.Models;

namespace Trabalho.Grpc.Servidor.Data
{
    public class CarrinhoContext
    {
        private readonly string[] Nomes = { "Sucrilhos", "Durex", "Band-aid", "Leite Ninho", "Bom Bril", "Gillette", "Miojo", "Cotonetes", "Yakult", "Maizena" };

        private CarrinhoCliente Carrinho;
        private readonly Random random = new();
        private bool voucher = false;

        public CarrinhoContext()
        {
        }

        public CarrinhoCliente ObterCarrinho(Guid id)
        {
            int quantidade = random.Next(0, 10);
            GerarCarrinho(quantidade, id);

            return Carrinho;
        }

        private void GerarCarrinho(int quantidade, Guid id)
        {
            voucher = !voucher;
            Carrinho = new CarrinhoCliente(id);
            for (int i = 0; i < quantidade; i++)
            {
                decimal valor = decimal.Round(Convert.ToDecimal(random.Next(5, 50) * random.NextDouble()), 2);
                Carrinho.AdicionarItem(new CarrinhoItem(Guid.NewGuid(), Nomes[random.Next(0, 9)], random.Next(1, 5), valor, "url"));
            }

            if (random.Next(0, 2) > 0)
            {
                string codigo = $"{Nomes[random.Next(0, 9)]}{DateTime.Now.Year}{random.Next(10, 50)}";
                TipoDescontoVoucher tipoDesconto = (TipoDescontoVoucher)random.Next(0, 2);
                Carrinho.AplicarVoucher(new Voucher(codigo, tipoDesconto, random));
            }
        }
    }
}
