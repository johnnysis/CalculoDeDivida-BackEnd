using CalculoDeDivida.Enums;
using CalculoDeDivida.Services;
using Microsoft.AspNetCore.SignalR;
using System;

namespace CalculoDeDivida.Models
{
    public class ConfiguracaoCalculo
    {
        public int MaximoParcelas { get; set; } //Quantidade máxima que a dívida pode ser parcelada
        public char TipoDeJuros { get; set; } //'S' - Simples, 'C' - Composto
        public decimal PorcentagemValorJuros { get; set; }
        public decimal PorcentagemComissao { get; set; }
    }
}