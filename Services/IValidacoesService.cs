using Domain.Interfaces;

namespace Services
{
  public interface IValidacoesService
  {
    bool ValidarFormaContida(IFormaContivel formaExterna, IFormaContivel formaInterna);
  }
}