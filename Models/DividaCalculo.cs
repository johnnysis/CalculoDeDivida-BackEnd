using CalculoDeDivida.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CalculoDeDivida.Models
{
    public abstract class DividaCalculo
    {
        public DateTime DataVencimento { get; set; }
        public int QuantidadeParcelas { get; set; }
        public decimal ValorOriginal { get; set; }
        public string TelefoneDeOrientacao { get
            {
                return "(11) 11111-1111"; //Não deu tempo de implementar a inserção no json.
            }
        }

        public int DiasAtraso { get; set; }
        public decimal ValorJuros { get; set; }
        public decimal ValorFinal { get; set; }
        public decimal ValorComissao { get; set; }
        public List<Parcela> Parcelas { get; set; }

        protected void CalculaDiasAtraso()
        {
            DiasAtraso = (int)DateTime.Now.Subtract(DataVencimento).TotalDays;
        }
        protected void CalculaValorFinal()
        {
            ValorFinal = ValorOriginal + ValorJuros + ValorComissao;
        }
        protected void CalculaComissao(decimal comissao)
        {
            ValorComissao = (ValorOriginal + ValorJuros) * comissao / 100;
        }
        protected void CalculaParcelas()
        {
            if (QuantidadeParcelas <= 0)
                QuantidadeParcelas = 1;

            decimal valorParcela = ValorFinal / QuantidadeParcelas;
            Parcelas = new List<Parcela>();
            for (int i = 0; i < QuantidadeParcelas; i++)
                Parcelas.Add(
                    new Parcela
                    {
                        ValorParcela = valorParcela,
                        DataVencimento = (DateTime.Now.AddDays(1).AddMonths(i))
                    }
                );
        }
        protected abstract void CalculaJuros(decimal porcentagemJuros);

        public void CalculaValores(ConfiguracaoCalculo configuracaoCalculo)
        {
            CalculaDiasAtraso();
            CalculaJuros(configuracaoCalculo.PorcentagemValorJuros);
            CalculaComissao(configuracaoCalculo.PorcentagemComissao);
            CalculaValorFinal();
            CalculaParcelas();
        }
    }
}
