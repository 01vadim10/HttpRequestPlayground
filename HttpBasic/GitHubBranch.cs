using System.Text.Json.Serialization;

namespace HttpBasic;

public record GitHubBranch([property: JsonPropertyName("name")] string Name);