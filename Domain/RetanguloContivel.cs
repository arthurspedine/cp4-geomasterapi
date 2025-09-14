using Domain.Interfaces;

namespace Domain
{
  public class RetanguloContivel : Retangulo, IFormaContivel
  {
    public RetanguloContivel(double largura, double altura) : base(largura, altura) { }

    public bool PodeConter(object formaInterna)
    {
      return formaInterna switch
      {
        CirculoContivel circuloInterno => RetanguloPodeConterCirculo(circuloInterno),
        RetanguloContivel retanguloInterno => RetanguloPodeConterRetangulo(retanguloInterno),
        _ => false
      };
    }

    private bool RetanguloPodeConterCirculo(CirculoContivel circulo)
    {
      // Para um retângulo conter um círculo, o diâmetro do círculo deve caber tanto na largura quanto na altura do retângulo
      var diametro = circulo.Raio * 2;
      return diametro <= this.Largura && diametro <= this.Altura;
    }

    private bool RetanguloPodeConterRetangulo(RetanguloContivel retanguloInterno)
    {
      // Um retângulo contém outro se suas dimensões forem maiores ou iguais
      // Considerando todas as orientações possíveis (rotação de 90°)

      // Orientação normal: largura em largura, altura em altura
      bool orientacaoNormal = retanguloInterno.Largura <= Largura &&
                         retanguloInterno.Altura <= Altura;

      // Orientação rotacionada: largura em altura, altura em largura
      bool orientacaoRotacionada = retanguloInterno.Largura <= Altura &&
                         retanguloInterno.Altura <= Largura;
      return orientacaoNormal || orientacaoRotacionada;
    }
  }
}