using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CalculoDeDivida.Enums;
using CalculoDeDivida.Models;
using CalculoDeDivida.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Logging;

namespace CalculoDeDivida.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculoController : ControllerBase
    {
        DadosDividasCliente dadosDividasCliente;
        DadosConfiguracaoDivida dadosConfiguracaoDivida;
        public CalculoController(DadosDividasCliente dadosDividasCliente, DadosConfiguracaoDivida dadosConfiguracaoDivida)
        {
            this.dadosConfiguracaoDivida = dadosConfiguracaoDivida;
            this.dadosDividasCliente = dadosDividasCliente;
        }
        [HttpGet]
        public async Task<ActionResult<DividaCalculo>> Get()
        {
            try
            {
                var configuracaoCalculo = await dadosConfiguracaoDivida.ConsultaConfiguracaoDivida();

                DividaCalculo dividaCalculo;
                if (configuracaoCalculo.TipoDeJuros == (char)TipoDeJuros.Simples)
                    dividaCalculo = await dadosDividasCliente.ConsultaDividasCliente<DividaCalculoJurosSimples>();
                else
                    dividaCalculo = await dadosDividasCliente.ConsultaDividasCliente< DividaCalculoJurosCompostos>();

                if(dividaCalculo != null)
                    dividaCalculo.CalculaValores(configuracaoCalculo);

                return dividaCalculo;
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new MensagemDeErro { Mensagem = ex.Message });
            }
        }
        [HttpPost]
        public async Task<ActionResult<DividaCalculo>> Post(CalculoEntrada calculoEntrada)
        {
            try
            {
                var configuracaoCalculo = await dadosConfiguracaoDivida.ConsultaConfiguracaoDivida();
                DividaCalculo dividaCalculo;

                if (configuracaoCalculo.TipoDeJuros == (char)TipoDeJuros.Simples)
                    dividaCalculo = await dadosDividasCliente.ConsultaDividasCliente<DividaCalculoJurosSimples>();
                else
                    dividaCalculo = await dadosDividasCliente.ConsultaDividasCliente<DividaCalculoJurosCompostos>();

                dividaCalculo.QuantidadeParcelas = calculoEntrada.QuantidadeParcelas;

                dividaCalculo.CalculaValores(configuracaoCalculo);

                await dadosDividasCliente.GravaDividasCliente(dividaCalculo);

                return dividaCalculo;
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new MensagemDeErro { Mensagem = ex.Message });
            }
        }
    }
}