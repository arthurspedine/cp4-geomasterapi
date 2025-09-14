namespace Domain
{
  public abstract class FormaCircular
  {
    public double Raio { get; protected set; }

    protected FormaCircular(double raio)
    {
      ValidarRaio(raio);
      Raio = raio;
    }
    private static void ValidarRaio(double raio)
    {
      if (raio <= 0)
        throw new ArgumentException("O raio deve ser maior que zero");
    }
  }
}