using Domain.Interfaces;

namespace Domain
{
  public class Esfera : FormaCircular, ICalculos3D
  {
    public Esfera(double raio) : base(raio)
    {
    }

    public double CalcularAreaSuperficial()
    {
      return 4 * Math.PI * Math.Pow(Raio, 2);
    }

    public double CalcularVolume()
    {
      return (4.0 / 3.0) * Math.PI * Math.Pow(Raio, 3);
    }
  }
}