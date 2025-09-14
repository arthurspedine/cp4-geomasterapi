using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dto
{
  public class FormaContidaRequestDto
  {
    [Required]
    [JsonPropertyName("formaExterna")]
    public FormaRequestDto FormaExterna { get; set; } = new();

    [Required]
    [JsonPropertyName("formaInterna")]
    public FormaRequestDto FormaInterna { get; set; } = new();
  }
}