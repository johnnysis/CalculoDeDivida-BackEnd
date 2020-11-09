using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculoDeDivida.Models
{
    public class DividaCalculoJurosCompostos : DividaCalculo
    {
        protected override void CalculaJuros(decimal porcentagemJuros)
        {
            ValorJuros = ValorOriginal * (decimal)Math.Pow(1 + (double)porcentagemJuros / 100, DiasAtraso) - ValorOriginal;
        }
    }
}