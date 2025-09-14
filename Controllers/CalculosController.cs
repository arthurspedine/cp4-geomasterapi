using Dto;
using Factory;
using Microsoft.AspNetCore.Mvc;
using Services;

[ApiController]
[Route("api/v1/calculos")]
[Produces("application/json")]
public class CalculosController : ControllerBase
{
  private readonly ICalculadoraService _calculadoraService;
  private readonly IFormaFactory _formaFactory;

  public CalculosController(
    ICalculadoraService calculadoraService,
    IFormaFactory formaFactory)
  {
    _calculadoraService = calculadoraService;
    _formaFactory = formaFactory;
  }

  /// <summary>
  /// Calcula a área de uma forma geométrica 2D
  /// </summary>
  /// <param name="request">Dados da forma geométrica</param>
  /// <returns>Resultado do cálculo da área</returns>
  /// <remarks>
  /// Exemplo de request para calcular área de um círculo:
  /// 
  ///     POST /api/v1/calculos/area
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
    try
    {
      // Validar se o tipo é suportado
      if (!_formaFactory.SuportaTipo(request.Tipo))
      {
        return BadRequest(new ProblemDetails
        {
          Title = "Tipo de forma não suportado",
          Detail = $"O tipo '{request.Tipo}' não é suportado. Tipos válidos: circulo, retangulo",
          Status = StatusCodes.Status400BadRequest
        });
      }

      // Criar a forma usando a factory
      var forma = _formaFactory.CriarForma(request.Tipo, request.Propriedades);

      // Calcular a área
      var area = _calculadoraService.CalcularArea(forma);

      return Ok(ResultadoCalculoDto.CriarResponse(request.Tipo, area, "area"));
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
    catch (InvalidOperationException ex)
    {
      return UnprocessableEntity(new ProblemDetails
      {
        Title = "Operação não suportada",
        Detail = ex.Message,
        Status = StatusCodes.Status422UnprocessableEntity
      });
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
      {
        Title = "Erro interno do servidor",
        Detail = "Ocorreu um erro inesperado ao processar a solicitação",
        Status = StatusCodes.Status500InternalServerError
      });
    }
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
    try
    {
      // Validar se o tipo é suportado
      if (!_formaFactory.SuportaTipo(request.Tipo))
      {
        return BadRequest(new ProblemDetails
        {
          Title = "Tipo de forma não suportado",
          Detail = $"O tipo '{request.Tipo}' não é suportado. Tipos válidos: circulo, retangulo",
          Status = StatusCodes.Status400BadRequest
        });
      }

      // Criar a forma usando a factory
      var forma = _formaFactory.CriarForma(request.Tipo, request.Propriedades);

      // Calcular o perímetro
      var perimetro = _calculadoraService.CalcularPerimetro(forma);

      return Ok(ResultadoCalculoDto.CriarResponse(request.Tipo, perimetro, "perimetro"));
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
    catch (InvalidOperationException ex)
    {
      return UnprocessableEntity(new ProblemDetails
      {
        Title = "Operação não suportada",
        Detail = ex.Message,
        Status = StatusCodes.Status422UnprocessableEntity
      });
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
      {
        Title = "Erro interno do servidor",
        Detail = "Ocorreu um erro inesperado ao processar a solicitação",
        Status = StatusCodes.Status500InternalServerError
      });
    }
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
    try
    {
      // Validar se o tipo é suportado
      if (!_formaFactory.SuportaTipo(request.Tipo))
      {
        return BadRequest(new ProblemDetails
        {
          Title = "Tipo de forma não suportado",
          Detail = $"O tipo '{request.Tipo}' não é suportado. Tipos válidos: esfera",
          Status = StatusCodes.Status400BadRequest
        });
      }

      // Criar a forma usando a factory
      var forma = _formaFactory.CriarForma(request.Tipo, request.Propriedades);

      // Calcular o volume
      var volume = _calculadoraService.CalcularVolume(forma);

      return Ok(ResultadoCalculoDto.CriarResponse(request.Tipo, volume, "volume"));
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
    catch (InvalidOperationException ex)
    {
      return UnprocessableEntity(new ProblemDetails
      {
        Title = "Operação não suportada",
        Detail = ex.Message,
        Status = StatusCodes.Status422UnprocessableEntity
      });
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
      {
        Title = "Erro interno do servidor",
        Detail = "Ocorreu um erro inesperado ao processar a solicitação",
        Status = StatusCodes.Status500InternalServerError
      });
    }
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
    try
    {
      // Validar se o tipo é suportado
      if (!_formaFactory.SuportaTipo(request.Tipo))
      {
        return BadRequest(new ProblemDetails
        {
          Title = "Tipo de forma não suportado",
          Detail = $"O tipo '{request.Tipo}' não é suportado. Tipos válidos: esfera",
          Status = StatusCodes.Status400BadRequest
        });
      }

      // Criar a forma usando a factory
      var forma = _formaFactory.CriarForma(request.Tipo, request.Propriedades);

      // Calcular a área superficial
      var areaSuperficial = _calculadoraService.CalcularAreaSuperficial(forma);

      return Ok(ResultadoCalculoDto.CriarResponse(request.Tipo, areaSuperficial, "area superficial"));
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
    catch (InvalidOperationException ex)
    {
      return UnprocessableEntity(new ProblemDetails
      {
        Title = "Operação não suportada",
        Detail = ex.Message,
        Status = StatusCodes.Status422UnprocessableEntity
      });
    }
    catch (Exception)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
      {
        Title = "Erro interno do servidor",
        Detail = "Ocorreu um erro inesperado ao processar a solicitação",
        Status = StatusCodes.Status500InternalServerError
      });
    }
  }
}