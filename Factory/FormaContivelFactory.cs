using System.Text.Json;
using Domain;
using Domain.Interfaces;

namespace Factory
{
  public class FormaContivelFactory : IFormaContivelFactory
  {

    private readonly Dictionary<string, Func<JsonElement, IFormaContivel>> _criadoresForma;

    public FormaContivelFactory()
    {
      _criadoresForma = new Dictionary<string, Func<JsonElement, IFormaContivel>>(StringComparer.OrdinalIgnoreCase)
      {
        ["circulo"] = CriarCirculoContivel,
        ["retangulo"] = CriarRetanguloContivel
      };
    }

    public IFormaContivel CriarFormaContivel(string tipo, JsonElement propriedades)
    {
      if (!_criadoresForma.TryGetValue(tipo, out var criador))
      {
        throw new ArgumentException($"Tipo de forma não suportado para contenção: {tipo}");
      }

      return criador(propriedades);
    }

    private IFormaContivel CriarCirculoContivel(JsonElement propriedades)
    {
      if (!propriedades.TryGetProperty("raio", out var raioElement) ||
            !raioElement.TryGetDouble(out var raio))
      {
        throw new ArgumentException("Propriedade 'raio' é obrigatória e deve ser um número válido");
      }

      if (raio <= 0)
      {
        throw new ArgumentException("O raio deve ser maior que zero");
      }

      return new CirculoContivel(raio);
    }

    private IFormaContivel CriarRetanguloContivel(JsonElement propriedades)
    {
      if (!propriedades.TryGetProperty("largura", out var larguraElement) ||
            !larguraElement.TryGetDouble(out var largura) ||
            !propriedades.TryGetProperty("altura", out var alturaElement) ||
            !alturaElement.TryGetDouble(out var altura))
      {
        throw new ArgumentException("Propriedades 'largura' e 'altura' são obrigatórias e devem ser números válidos");
      }

      if (largura <= 0 || altura <= 0)
      {
        throw new ArgumentException("Largura e altura devem ser maiores que zero");
      }

      return new RetanguloContivel(largura, altura);
    }
    
  }
}