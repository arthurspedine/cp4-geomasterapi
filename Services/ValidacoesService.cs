using Domain.Interfaces;

namespace Services
{
  public class ValidacoesService : IValidacoesService
  {
    public bool ValidarFormaContida(IFormaContivel formaExterna, IFormaContivel formaInterna)
    {
      return formaExterna.PodeConter(formaInterna);
    }
  }
}