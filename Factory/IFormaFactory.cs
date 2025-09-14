using System.Text.Json;

namespace Factory
{
  public interface IFormaFactory
  {
    object CriarForma(string tipo, JsonElement propriedades);
    bool SuportaTipo(string tipo);
  }
}