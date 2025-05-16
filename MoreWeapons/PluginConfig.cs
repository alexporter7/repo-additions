using System;
using System.Collections.Generic;
using BepInEx.Configuration;
using MoreWeapons.Enums;
using MoreWeapons.Preset;

namespace MoreWeapons;

public class PluginConfig {
    public static Dictionary<AmmoClipSize, ConfigEntry<int>> AmmoClipOptions;

    public static void Load(ConfigFile config) {
        
        /*
         * Ammo Clip Options
         */
        foreach (AmmoClipSize preset in Enum.GetValues(typeof(AmmoClipSize))) {
            AmmoClipPresets.AmmoClipPreset aPreset = AmmoClipPresets.Presets[preset];
            AmmoClipOptions.Add(preset, config.Bind<int>(
                "Ammo Clips",
                aPreset.PresetName + " Clip",
                aPreset.DefaultSize,
                aPreset.PresetDescription));
        }
        
    }
}