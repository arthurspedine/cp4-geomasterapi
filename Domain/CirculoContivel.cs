using Domain.Interfaces;

namespace Domain
{
  public class CirculoContivel : Circulo, IFormaContivel
  {
    public CirculoContivel(double raio) : base(raio) { }

    public bool PodeConter(object formaInterna)
    {
      return formaInterna switch
      {
        CirculoContivel circuloInterno => CirculoPodeConterCirculo(circuloInterno),
        RetanguloContivel retanguloInterno => CirculoPodeConterRetangulo(retanguloInterno),
        _ => false
      };
    }

    private bool CirculoPodeConterCirculo(CirculoContivel circuloInterno)
    {
      // Um círculo contém outro se o raio do círculo interno for menor ou igual ao raio do círculo externo
      return Raio >= circuloInterno.Raio;
    }

    private bool CirculoPodeConterRetangulo(RetanguloContivel retangulo)
    {
      // Para um círculo conter um retângulo, a diagonal do retângulo deve caber no diâmetro do círculo
      var diagonal = Math.Sqrt(Math.Pow(retangulo.Largura, 2) + Math.Pow(retangulo.Altura, 2));
      var raioNecessario = diagonal / 2.0;
      return raioNecessario <= Raio;
    }
  }
}