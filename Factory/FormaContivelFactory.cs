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
      var raio = ValidadorPropriedades.ValidarRaio(propriedades);
      return new CirculoContivel(raio);
    }

    private IFormaContivel CriarRetanguloContivel(JsonElement propriedades)
    {
      var (largura, altura) = ValidadorPropriedades.ValidarLarguraAltura(propriedades);
      return new RetanguloContivel(largura, altura);
    }
    
  }
}