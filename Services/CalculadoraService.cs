using Domain;
using Domain.Interfaces;

namespace Services
{
  public class CalculadoraService : ICalculadoraService
  {
    public double CalcularArea(object forma)
    {
      return forma switch
      {
        ICalculos2D forma2D => forma2D.CalcularArea(),
        _ => throw new InvalidOperationException($"A forma {forma.GetType().Name} não suporta cálculo de área 2D")
      };
    }

    public double CalcularPerimetro(object forma)
    {
      return forma switch
      {
        ICalculos2D forma2D => forma2D.CalcularPerimetro(),
        _ => throw new InvalidOperationException($"A forma {forma.GetType().Name} não suporta cálculo de perímetro")
      };
    }

    public double CalcularVolume(object forma)
    {
      return forma switch
      {
        ICalculos3D forma3D => forma3D.CalcularVolume(),
        _ => throw new InvalidOperationException($"A forma {forma.GetType().Name} não suporta cálculo de volume")
      };
    }

    public double CalcularAreaSuperficial(object forma)
    {
      return forma switch
      {
        ICalculos3D forma3D => forma3D.CalcularAreaSuperficial(),
        _ => throw new InvalidOperationException($"A forma {forma.GetType().Name} não suporta cálculo de área superficial")
      };
    }
  }
}