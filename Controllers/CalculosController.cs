using Dto;
using Factory;
using Microsoft.AspNetCore.Mvc;
using Services;
using Controllers;

[ApiController]
[Route("api/v1/calculos")]
[Produces("application/json")]
public class CalculosController : BaseCalculosController
{
  public CalculosController(
    ICalculadoraService calculadoraService,
    IFormaFactory formaFactory) : base(calculadoraService, formaFactory)
  {
  }

  /// <summary>
  /// Calcula a área de uma forma geométrica 2D
  /// </summary>
  /// <param name="request">Dados da forma geométrica</param>
  /// <returns>Resultado do cálculo da área</returns>
  /// <remarks>
  /// Exemplo de request para calcular área de um círculo:
  /// 
  ///     {
  ///         "tipoForma": "circulo",
  ///         "propriedades": {
  ///             "raio": 5.0
  ///         }
  ///     }
  /// 
  /// Exemplo para retângulo:
  /// 
  ///     {
  ///         "tipoForma": "retangulo",
  ///         "propriedades": {
  ///             "largura": 10.0,
  ///             "altura": 8.0
  ///         }
  ///     }
  /// 
  /// Tipos suportados: circulo, retangulo
  /// </remarks>
  /// <response code="200">Cálculo realizado com sucesso</response>
  /// <response code="400">Dados de entrada inválidos</response>
  /// <response code="422">Operação não suportada para o tipo de forma</response>
  /// <response code="500">Erro interno do servidor</response>
  [HttpPost("area")]
  [ProducesResponseType(typeof(ResultadoCalculoDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public IActionResult CalcularArea([FromBody] FormaRequestDto request)
  {
    return ExecutarCalculo(request, _calculadoraService.CalcularArea, "area", "circulo, retangulo");
  }

  /// <summary>
  /// Calcula o perímetro de uma forma geométrica 2D
  /// </summary>
  /// <param name="request">Dados da forma geométrica</param>
  /// <returns>Resultado do cálculo do perímetro</returns>
  /// <remarks>
  /// Exemplo de request para calcular perímetro de um círculo:
  /// 
  ///     {
  ///         "tipoForma": "circulo",
  ///         "propriedades": {
  ///             "raio": 4.0
  ///         }
  ///     }
  /// 
  /// Exemplo para retângulo:
  /// 
  ///     {
  ///         "tipoForma": "retangulo",
  ///         "propriedades": {
  ///             "largura": 8.0,
  ///             "altura": 12.0
  ///         }
  ///     }
  /// 
  /// Tipos suportados: circulo, retangulo
  /// </remarks>
  /// <response code="200">Cálculo realizado com sucesso</response>
  /// <response code="400">Dados de entrada inválidos</response>
  /// <response code="422">Operação não suportada para o tipo de forma</response>
  /// <response code="500">Erro interno do servidor</response>
  [HttpPost("perimetro")]
  [ProducesResponseType(typeof(ResultadoCalculoDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public IActionResult CalcularPerimetro([FromBody] FormaRequestDto request)
  {
    return ExecutarCalculo(request, _calculadoraService.CalcularPerimetro, "perimetro", "circulo, retangulo");
  }

  /// <summary>
  /// Calcula o volume de uma forma geométrica 3D
  /// </summary>
  /// <param name="request">Dados da forma geométrica</param>
  /// <returns>Resultado do cálculo do volume</returns>
  /// <remarks>
  /// Exemplo de request para calcular volume de uma esfera:
  /// 
  ///     {
  ///         "tipoForma": "esfera",
  ///         "propriedades": {
  ///             "raio": 8.0
  ///         }
  ///     }
  /// 
  /// Tipos suportados: esfera
  /// </remarks>
  /// <response code="200">Cálculo realizado com sucesso</response>
  /// <response code="400">Dados de entrada inválidos</response>
  /// <response code="422">Operação não suportada para o tipo de forma</response>
  /// <response code="500">Erro interno do servidor</response>
  [HttpPost("volume")]
  [ProducesResponseType(typeof(ResultadoCalculoDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public IActionResult CalcularVolume([FromBody] FormaRequestDto request)
  {
    return ExecutarCalculo(request, _calculadoraService.CalcularVolume, "volume", "esfera");
  }
  
  /// <summary>
  /// Calcula a área superficial de uma forma geométrica 3D
  /// </summary>
  /// <param name="request">Dados da forma geométrica</param>
  /// <returns>Resultado do cálculo da área superficial</returns>
  /// <remarks>
  /// Exemplo de request para calcular área superficial de uma esfera:
  /// 
  ///     {
  ///         "tipoForma": "esfera",
  ///         "propriedades": {
  ///             "raio": 8.0
  ///         }
  ///     }
  /// 
  /// Tipos suportados: esfera
  /// </remarks>
  /// <response code="200">Cálculo realizado com sucesso</response>
  /// <response code="400">Dados de entrada inválidos</response>
  /// <response code="422">Operação não suportada para o tipo de forma</response>
  /// <response code="500">Erro interno do servidor</response>
  [HttpPost("area-superficial")]
  [ProducesResponseType(typeof(ResultadoCalculoDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public IActionResult CalcularAreaSuperficial([FromBody] FormaRequestDto request)
  {
    return ExecutarCalculo(request, _calculadoraService.CalcularAreaSuperficial, "area superficial", "esfera");
  }
}