using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dto
{
  public class FormaRequestDto
  {
    [Required]
    [JsonPropertyName("tipoForma")]
    public string Tipo { get; set; } = string.Empty;

    [Required]
    [JsonPropertyName("propriedades")]
    public JsonElement Propriedades { get; set; }
  }
}