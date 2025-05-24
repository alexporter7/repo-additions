using BepInEx.Configuration;

namespace AModLib.Utils;

public static class ConfigUtil {

    public static ConfigEntry<T> GetConfigOption<T>(ConfigFile config, string section, string key) {
        config.TryGetEntry<T>(section, key, out ConfigEntry<T> configEntry);
        return configEntry;
    }

    public static T GetConfigOptionValue<T>(ConfigFile config, string section, string key) {
        config.TryGetEntry<T>(section, key, out ConfigEntry<T> configEntry);
        return configEntry.Value;
    }

}