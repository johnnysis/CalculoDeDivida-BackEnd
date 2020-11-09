using CalculoDeDivida.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CalculoDeDivida.Services
{
    public class DadosDividasCliente : Dados
    {
        public DadosDividasCliente(IConfiguration configuration)
            : base(configuration.GetValue<string>("DadosDividasCliente"))
        {
        }

        public async Task<T> ConsultaDividasCliente<T>() => await Consulta<T>();
        public async Task GravaDividasCliente(DividaCalculo dividaCalculo) => await Grava(dividaCalculo); //Para depois - fazer gravar em um array.
    }
}