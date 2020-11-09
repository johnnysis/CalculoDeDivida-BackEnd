namespace CalculoDeDivida.Models
{
    public class DividaCalculoJurosSimples : DividaCalculo
    {
        protected override void CalculaJuros(decimal porcentagemJuros)
        {
            ValorJuros = ValorOriginal * porcentagemJuros / 100 * DiasAtraso;
        }
    }
}