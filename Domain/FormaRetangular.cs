namespace Domain
{
  public abstract class FormaRetangular
  {
    public double Largura { get; protected set; }

    public double Altura { get; protected set; }

    protected FormaRetangular(double largura, double altura)
    {
      ValidarDimensoes(largura, altura);
      Largura = largura;
      Altura = altura;
    }

    private static void ValidarDimensoes(double largura, double altura)
    {
      if (largura <= 0 || altura <= 0)
        throw new ArgumentException("Largura e altura devem ser maiores que zero");
    }
  }
}