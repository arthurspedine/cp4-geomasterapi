using Domain.Interfaces;

namespace Domain
{
  public class Circulo : ICalculos2D
  {
    public double Raio { get; set; }

    public Circulo(double raio)
    {
      Raio = raio;
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