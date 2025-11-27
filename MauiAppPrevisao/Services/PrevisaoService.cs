using System.Collections.Generic;
using System.Linq;
using MauiAppPrevisao.Models;

namespace MauiAppPrevisao.Services
{
    public class PrevisaoService
    {
        public List<WeatherMock> PrevisoesMock { get; private set; }

        public PrevisaoService()
        {
            InicializarPrevisoesMock();
        }

        private void InicializarPrevisoesMock()
        {
            PrevisoesMock = new List<WeatherMock>
            {
                new WeatherMock { Cidade = "São Paulo", Temperatura = 25, Umidade = 60, Condicao = "Ensolarado" },
                new WeatherMock { Cidade = "Rio de Janeiro", Temperatura = 30, Umidade = 70, Condicao = "Parcialmente nublado" },
                new WeatherMock { Cidade = "Curitiba", Temperatura = 18, Umidade = 80, Condicao = "Chuva fraca" },
                new WeatherMock { Cidade = "Recife", Temperatura = 29, Umidade = 75, Condicao = "Ensolarado" },
                new WeatherMock { Cidade = "Jaú", Temperatura = 28, Umidade = 55, Condicao = "Ensolarado" },
                new WeatherMock { Cidade = "Salvador", Temperatura = 28, Umidade = 82, Condicao = "Tempo abafado" }
            };
        }

        public WeatherMock Buscar(string cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade)) return null;
            return PrevisoesMock
                .FirstOrDefault(p => p.Cidade.ToLower().Contains(cidade.Trim().ToLower()));
        }
    }
}
