using BepInEx.Configuration;

namespace AModLib;

public static class PluginConfig {

    private static ConfigFile Config;

    public static void LoadConfigModules(ConfigFile config) {
        Config = config;
        LoadLoggingConfig();
        LoadStatisticsConfig();
    }

    public static void LoadLoggingConfig() {
        Config.Bind(
            "Log Settings",
            "Log Debug Events",
            false,
            "Show Debug log events");
    }
    public static void LoadStatisticsConfig() {
        
    }

}