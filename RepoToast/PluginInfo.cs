using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RepoToast;

public class PluginInfo {

    public static PluginInfo Instance = new();
    
    [JsonProperty]
    public static string Name = "RepoToast";

    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public static string VersionNumber = "0.1.0";
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public static string WebsiteUrl = "https://github.com/alexporter7/gaymers-repo-pack";

    [JsonProperty]
    public static string Description = "Toast notifications for different events throughout the game.";

    [JsonProperty]
    public static string[] Dependencies = ["nickklmao-MenuLib-2.4.1"];

}