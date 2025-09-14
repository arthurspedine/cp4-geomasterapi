using System.Text.Json;
using Domain.Interfaces;

namespace Factory
{
  public interface IFormaContivelFactory
  {
    IFormaContivel CriarFormaContivel(string tipo, JsonElement propriedades);
  }
}