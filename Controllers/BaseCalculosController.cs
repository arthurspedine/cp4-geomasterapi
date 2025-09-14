using Dto;
using Factory;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
  public abstract class BaseCalculosController : ControllerBase
  {
    protected readonly ICalculadoraService _calculadoraService;
    protected readonly IFormaFactory _formaFactory;

    protected BaseCalculosController(
        ICalculadoraService calculadoraService,
        IFormaFactory formaFactory)
    {
      _calculadoraService = calculadoraService;
      _formaFactory = formaFactory;
    }

    protected IActionResult ExecutarCalculo(
        FormaRequestDto request,
        Func<object, double> operacao,
        string nomeOperacao,
        string tiposSuportados)
    {
      try
      {
        // Validar se o tipo é suportado
        if (!_formaFactory.SuportaTipo(request.Tipo))
        {
          return BadRequest(new ProblemDetails
          {
            Title = "Tipo de forma não suportado",
            Detail = $"O tipo '{request.Tipo}' não é suportado. Tipos válidos: {tiposSuportados}",
            Status = StatusCodes.Status400BadRequest
          });
        }

        // Criar a forma usando a factory
        var forma = _formaFactory.CriarForma(request.Tipo, request.Propriedades);

        // Executar o cálculo
        var resultado = operacao(forma);

        return Ok(ResultadoCalculoDto.CriarResponse(request.Tipo, resultado, nomeOperacao));
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
}