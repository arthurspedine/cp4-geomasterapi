using Domain.Interfaces;

namespace Domain
{
  public class Retangulo : FormaRetangular, ICalculos2D
  {
    public Retangulo(double largura, double altura) : base(largura, altura)
    {
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