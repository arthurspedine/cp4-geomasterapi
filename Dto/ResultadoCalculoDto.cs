namespace Dto
{
  public class ResultadoCalculoDto
  {
    public string TipoForma { get; set; } = string.Empty;
    public double Resultado { get; set; }
    public string Operacao { get; set; } = string.Empty;
    public DateTime DataCalculo { get; set; } = DateTime.UtcNow;


    public static ResultadoCalculoDto CriarResponse(string tipoForma, double resultado, string operacao)
    {
      return new ResultadoCalculoDto
      {
        TipoForma = tipoForma,
        Resultado = Math.Round(resultado, 4), // Arredondar para 4 casas decimais
        Operacao = operacao
      };
    }
  }
}