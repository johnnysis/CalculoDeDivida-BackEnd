using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace CalculoDeDivida.Services
{
    public abstract class Dados
    {
        private readonly string _caminhoArquivo;
        public Dados(string caminhoArquivo)
        {
            _caminhoArquivo = caminhoArquivo;
        }
        protected async Task<T> Consulta<T>()
        {
            try
            {
                if (File.Exists(_caminhoArquivo))
                {
                    using (var fs = File.OpenRead(_caminhoArquivo))
                        return await JsonSerializer.DeserializeAsync<T>(fs);
                }
            }
            catch(Exception ex)
            {
            }
            return default(T);
        }

        protected async Task Grava<T>(T objeto)
        {
            using (var fs = File.Create(_caminhoArquivo))
                await JsonSerializer.SerializeAsync(fs, objeto);
        }
    }
}
