using Newtonsoft.Json;

namespace BuildRepoAdditions;

public class ModManifest {
    
    [JsonProperty("name")]           public string   Name;
    [JsonProperty("description")]    public string   Description;
    [JsonProperty("version_number")] public string   VersionNumber;
    [JsonProperty("dependencies")]   public string[] Dependencies;
    [JsonProperty("website_url")]    public string[] WebsiteUrl;
    
}