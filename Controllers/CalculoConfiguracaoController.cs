using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CalculoDeDivida.Models;
using CalculoDeDivida.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalculoDeDivida.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracaoCalculoController : ControllerBase
    {
        private DadosConfiguracaoDivida dadosConfiguracaoDivida;
        public ConfiguracaoCalculoController(DadosConfiguracaoDivida dadosConfiguracaoDivida)
        {
            this.dadosConfiguracaoDivida = dadosConfiguracaoDivida;
        }

        [HttpGet]
        public async Task<ActionResult<ConfiguracaoCalculo>> Get()
        {
            var configuracaoCalculo = await dadosConfiguracaoDivida.ConsultaConfiguracaoDivida();
            return Ok(configuracaoCalculo);
        }
        [HttpPost]
        public async Task<ActionResult> Post(ConfiguracaoCalculo configuracaoDivida)
        {
            await dadosConfiguracaoDivida.GravaConfiguracaoDivida(configuracaoDivida);
            return Ok();
        }
    }
}
