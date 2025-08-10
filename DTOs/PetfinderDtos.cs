using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RuedaYPatas.DTOs;

public class PetfinderTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
}

public class PetfinderBreedsResponse
{
    [JsonPropertyName("breeds")]
    public IEnumerable<PetfinderBreed> Breeds { get; set; }
}

public class PetfinderBreed
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}