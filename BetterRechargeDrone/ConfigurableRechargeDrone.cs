using BepInEx.Configuration;

namespace BetterRechargeDrone;

public class ConfigurableRechargeDrone {

    private ConfigEntry<bool> droneEnabled { get; set; }
    private ConfigEntry<int>  batterySize  { get; set; }
    private ConfigEntry<int>  minimumPrice { get; set; }
    private ConfigEntry<int>  maximumPrice { get; set; }
    private ConfigEntry<string> droneName { get; set; }
    
}