using Domain.Interfaces;

namespace Domain
{
  public class Retangulo : ICalculos2D
  {
    public double Largura { get; set; }
    public double Altura { get; set; }

    public Retangulo(double largura, double altura)
    {
      Largura = largura;
      Altura = altura;
    }

    public double CalcularArea()
    {
      return Largura * Altura;
    }

    public double CalcularPerimetro()
    {
      return 2 * (Largura + Altura);
    }
  }
}