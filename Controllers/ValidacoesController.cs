using Domain.Interfaces;
using Dto;
using Factory;
using Microsoft.AspNetCore.Mvc;
using Services;

[ApiController]
[Route("api/v1/validacoes")]
[Produces("application/json")]
public class ValidacoesController : ControllerBase
{
  private readonly IValidacoesService _validacoesService;
  private readonly IFormaContivelFactory _formaFactory;

  public ValidacoesController(
      IValidacoesService validacoesService,
      IFormaContivelFactory formaFactory)
  {
    _validacoesService = validacoesService;
    _formaFactory = formaFactory;
  }

  /// <summary>
  /// Valida se uma forma geométrica pode ser contida dentro de outra
  /// </summary>
  /// <param name="request">Dados das formas externa e interna</param>
  /// <returns>Resultado da validação de contenção</returns>
  /// <remarks>
  /// Exemplos de validação:
  /// 
  /// Círculo em Retângulo:
  /// 
  ///     {
  ///         "formaExterna": {
  ///             "tipoForma": "retangulo",
  ///             "propriedades": {
  ///                 "largura": 10.0,
  ///                 "altura": 10.0
  ///             }
  ///         },
  ///         "formaInterna": {
  ///             "tipoForma": "circulo",
  ///             "propriedades": {
  ///                 "raio": 5.0
  ///             }
  ///         }
  ///     }
  /// 
  /// Retângulo em Círculo:
  /// 
  ///     {
  ///         "formaExterna": {
  ///             "tipoForma": "circulo",
  ///             "propriedades": {
  ///                 "raio": 2.0
  ///             }
  ///         },
  ///         "formaInterna": {
  ///             "tipoForma": "retangulo",
  ///             "propriedades": {
  ///                 "largura": 2.0,
  ///                 "altura": 3.0
  ///             }
  ///         }
  ///     }
  /// 
  /// A validação usa algoritmos matemáticos precisos:
  /// - Para círculo conter retângulo: diagonal do retângulo ≤ diâmetro do círculo
  /// - Para retângulo conter círculo: diâmetro do círculo ≤ menor dimensão do retângulo
  /// - Para retângulo conter retângulo: considera rotações de 90°
  /// </remarks>
  /// <response code="200">Validação realizada com sucesso</response>
  /// <response code="400">Dados de entrada inválidos</response>
  /// <response code="500">Erro interno do servidor</response>
  [HttpPost("forma-contida")]
  [ProducesResponseType(typeof(FormaContidaRequestDto), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
  public IActionResult ValidarFormaContida(
    [FromBody] FormaContidaRequestDto request)
  {
    try
    {
      IFormaContivel formaExterna = _formaFactory.CriarFormaContivel(request.FormaExterna.Tipo, request.FormaExterna.Propriedades);
      IFormaContivel formaInterna = _formaFactory.CriarFormaContivel(request.FormaInterna.Tipo, request.FormaInterna.Propriedades);

      bool resultado = _validacoesService.ValidarFormaContida(formaExterna, formaInterna);
      return Ok(new { resultado });
    }
    catch (ArgumentException ex)
    {
      return BadRequest(new ProblemDetails
      {
        Title = "Dados de entrada inválidos",
        Detail = ex.Message,
        Status = StatusCodes.Status400BadRequest
      });
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
      {
        Title = "Erro interno do servidor",
        Detail = "Ocorreu um erro inesperado ao processar a validação",
        Status = StatusCodes.Status500InternalServerError
      });
    }
  }
}