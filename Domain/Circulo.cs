using Domain.Interfaces;

namespace Domain
{
  public class Circulo : FormaCircular, ICalculos2D
  {
    public Circulo(double raio) : base(raio)
    {
    }

    public double CalcularArea()
    {
      return Math.PI * Math.Pow(Raio, 2);
    }

    public double CalcularPerimetro()
    {
      return 2 * Math.PI * Raio;
    }
  }
}