namespace Trabalho.Grpc.Servidor.Models
{
    public class Voucher
    {
        public decimal? Percentual { get; set; }
        public decimal? ValorDesconto { get; set; }
        public string Codigo { get; set; }
        public TipoDescontoVoucher TipoDesconto { get; set; }

        //O Voucher normalmente existiria no banco
        public Voucher(string codigo, TipoDescontoVoucher tipoDesconto, Random rand)
        {
            Codigo = codigo;
            switch (tipoDesconto)
            {
                case TipoDescontoVoucher.Porcentagem:
                    Percentual = rand.Next(1, 25);
                    break;
                case TipoDescontoVoucher.Valor:
                    ValorDesconto = rand.Next(10, 50);
                    break;
                default:
                    throw new ArgumentNullException("Tipo Desconto para o Voucher desconhecido ou Nulo.");
            }
        }
    }

    public enum TipoDescontoVoucher
    {
        Porcentagem = 0,
        Valor = 1
    }
}
