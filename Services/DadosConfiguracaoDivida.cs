using CalculoDeDivida.Enums;
using CalculoDeDivida.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CalculoDeDivida.Services
{
    public class DadosConfiguracaoDivida : Dados
    {
        public DadosConfiguracaoDivida(IConfiguration configuration)
            : base(configuration.GetValue<string>("DadosConfiguracaoCalculo"))
        {
        }

        public async Task<ConfiguracaoCalculo> ConsultaConfiguracaoDivida()
        {
            var configuracao = await Consulta<ConfiguracaoCalculo>();
            if (configuracao == null)
            {
                configuracao = new ConfiguracaoCalculo
                {
                    MaximoParcelas = 10,
                    TipoDeJuros = (char)TipoDeJuros.Simples
                }; //Configuração default.
                await Grava(configuracao);
            }


            return configuracao;
        }

        public async Task GravaConfiguracaoDivida(ConfiguracaoCalculo configuracaoCalculo) => await Grava(configuracaoCalculo);
    }
}