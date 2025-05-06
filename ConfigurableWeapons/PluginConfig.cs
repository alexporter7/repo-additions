using BepInEx.Configuration;

namespace ConfigurableWeapons;

public class PluginConfig {
    
    public static float MaxCharge = 120f;
    
    public static ConfigEntry<int> TranqMaxShots;
    public static ConfigEntry<int> ShotgunMaxShots;
    public static ConfigEntry<int> HandgunMaxShots;
    public static ConfigEntry<int> GunMaxShots;

    public static void Load(ConfigFile config) {
        TranqMaxShots = config.Bind<int>("General", "Tranq Gun Max Shots", 10,
            "How many shots the Tranq Gun can shoot on a full charge");
        ShotgunMaxShots = config.Bind<int>("General", "Tranq Gun Max Shots", 10,
            "How many shots the Tranq Gun can shoot on a full charge");
        HandgunMaxShots = config.Bind<int>("General", "Tranq Gun Max Shots", 10,
            "How many shots the Tranq Gun can shoot on a full charge");
        GunMaxShots = config.Bind<int>("General", "Tranq Gun Max Shots", 10,
            "How many shots the Tranq Gun can shoot on a full charge");
    }

    public static float GetDrainCharge(int maxShots) {
        return MaxCharge / maxShots;
    }
}