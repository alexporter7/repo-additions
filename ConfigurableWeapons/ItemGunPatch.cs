using BepInEx.Configuration;
using HarmonyLib;

namespace ConfigurableWeapons;

[HarmonyPatch(typeof(ItemGun), "Update")]
public class ItemGunPatch {

    public static void Prefix(ItemGun __instance) {
        if (__instance == null)
            return;

        if (__instance.name.Contains("Item Gun Tranq")) {
            __instance.batteryDrain         = PluginConfig.GetDrainCharge(PluginConfig.TranqMaxShots.Value);
            __instance.batteryDrainFullBar  = false;
            __instance.batteryDrainFullBars = 1;
        }
        else if (__instance.name.Contains("Item Gun Shotgun")) {
            __instance.batteryDrain         = PluginConfig.GetDrainCharge(PluginConfig.ShotgunMaxShots.Value);;
            __instance.batteryDrainFullBar  = false;
            __instance.batteryDrainFullBars = 1;
        }
        else if (__instance.name.Contains("Item Gun Handgun")) {
            __instance.batteryDrain         = PluginConfig.GetDrainCharge(PluginConfig.HandgunMaxShots.Value);;
            __instance.batteryDrainFullBar  = false;
            __instance.batteryDrainFullBars = 1;
        }
        else if (__instance.name.Contains("Item Gun")) {
            __instance.batteryDrain         = PluginConfig.GetDrainCharge(PluginConfig.GunMaxShots.Value);;
            __instance.batteryDrainFullBar  = false;
            __instance.batteryDrainFullBars = 1;
        }
    }
}