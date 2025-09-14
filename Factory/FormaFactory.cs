using System.Text.Json;
using Domain;

namespace Factory
{
  public class FormaFactory : IFormaFactory
  {

    private readonly Dictionary<string, Func<JsonElement, object>> _criadoresForma;

    public FormaFactory()
    {
      _criadoresForma = new Dictionary<string, Func<JsonElement, object>>(StringComparer.OrdinalIgnoreCase)
      {
        ["circulo"] = CriarCirculo,
        ["retangulo"] = CriarRetangulo,
        ["esfera"] = CriarEsfera
      };
    }

    public object CriarForma(string tipo, JsonElement propriedades)
    {
      if (!_criadoresForma.TryGetValue(tipo, out var criador))
      {
        throw new ArgumentException($"Tipo de forma '{tipo}' não é suportado");
      }
      return criador(propriedades);
    }

    public bool SuportaTipo(string tipo)
    {
      return _criadoresForma.ContainsKey(tipo);
    }
    
    private object CriarCirculo(JsonElement props)
    {
        if (!props.TryGetProperty("raio", out var raioElement) || 
            !raioElement.TryGetDouble(out var raio))
        {
            throw new ArgumentException("Propriedade 'raio' é obrigatória e deve ser um número válido");
        }

        if (raio <= 0)
        {
            throw new ArgumentException("O raio deve ser maior que zero");
        }

        return new Circulo(raio);
    }

    private object CriarRetangulo(JsonElement props)
    {
        if (!props.TryGetProperty("largura", out var larguraElement) || 
            !larguraElement.TryGetDouble(out var largura) ||
            !props.TryGetProperty("altura", out var alturaElement) || 
            !alturaElement.TryGetDouble(out var altura))
        {
            throw new ArgumentException("Propriedades 'largura' e 'altura' são obrigatórias e devem ser números válidos");
        }

        if (largura <= 0 || altura <= 0)
        {
            throw new ArgumentException("Largura e altura devem ser maiores que zero");
        }

        return new Retangulo(largura, altura);
    }

    private object CriarEsfera(JsonElement props)
    {
        if (!props.TryGetProperty("raio", out var raioElement) || 
            !raioElement.TryGetDouble(out var raio))
        {
            throw new ArgumentException("Propriedade 'raio' é obrigatória e deve ser um número válido");
        }

        if (raio <= 0)
        {
            throw new ArgumentException("O raio deve ser maior que zero");
        }

        return new Esfera(raio);
    }
  }
}