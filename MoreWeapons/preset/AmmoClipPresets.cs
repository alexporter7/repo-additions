using System.Collections.Generic;

namespace MoreWeapons.preset;

public class AmmoClipPresets {
    public static Dictionary<AmmoClipSize, AmmoClipPreset> Presets =
        new() {
            { AmmoClipSize.Small, new AmmoClipPreset("Small", 8) },
            { AmmoClipSize.Medium, new AmmoClipPreset("Medium", 12) },
            { AmmoClipSize.Large, new AmmoClipPreset("Large", 16) },
            { AmmoClipSize.XLarge, new AmmoClipPreset("XLarge", 20) }
        };

    public class AmmoClipPreset(string name, int defaultSize) {
        public string PresetName        = name;
        public int    DefaultSize       = defaultSize;
        public string PresetDescription = "How many rounds of ammo are contained in the " + name + " clip";
    }
}