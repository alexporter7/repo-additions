using BepInEx.Configuration;
using HarmonyLib;

namespace ConfigurableWeapons;

[HarmonyPatch(typeof(ItemGun))]
public class ItemGunPatch {
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    public static void Postfix(ItemGun __instance) {
        if (__instance == null)
            return;

        if (__instance.name.Contains("Item Gun Tranq")) {
            __instance.batteryDrain         = PluginConfig.GetDrainCharge(PluginConfig.TranqMaxShots.Value);
            __instance.batteryDrainFullBar  = false;
            __instance.batteryDrainFullBars = 1;
        }
        else if (__instance.name.Contains("Item Gun Shotgun")) {
            __instance.batteryDrain         = PluginConfig.GetDrainCharge(PluginConfig.ShotgunMaxShots.Value);
            __instance.batteryDrainFullBar  = false;
            __instance.batteryDrainFullBars = 1;
        }
        else if (__instance.name.Contains("Item Gun Handgun")) {
            ConfigurableWeapons.Logger.LogInfo("Pre: " + __instance.itemBattery.batteryLife + " | " +  __instance.itemBattery.batteryLifeInt + " | Drain " + __instance.batteryDrain + " | Shots: " + __instance.numberOfBullets);
            __instance.batteryDrain        = PluginConfig.GetDrainCharge(PluginConfig.HandgunMaxShots.Value);
            __instance.batteryDrainFullBar = false;
            ConfigurableWeapons.Logger.LogInfo("Post: " + __instance.itemBattery.batteryLife + " | " +  __instance.itemBattery.batteryLifeInt + " | Drain " + __instance.batteryDrain + " | Shots: " + __instance.numberOfBullets);
            // __instance.batteryDrainFullBars = 1;
            
        }
        else if (__instance.name.Contains("Item Gun")) {
            __instance.batteryDrain         = PluginConfig.GetDrainCharge(PluginConfig.GunMaxShots.Value);
            __instance.batteryDrainFullBar  = false;
            __instance.batteryDrainFullBars = 1;
            
        }
    }

    [HarmonyPatch("Shoot")]
    [HarmonyPrefix]
    public static void ShootEvent(ItemGun __instance) {
        if (__instance.name.Contains("Item Gun Handgun")) {
            ConfigurableWeapons.Logger.LogInfo("Pre: " + __instance.itemBattery.batteryLife + " | " +  __instance.itemBattery.batteryLifeInt + " | Drain " + __instance.batteryDrain);
            __instance.batteryDrain = PluginConfig.GetDrainCharge(PluginConfig.HandgunMaxShots.Value);
            ConfigurableWeapons.Logger.LogInfo("Pre: " + __instance.itemBattery.batteryLife + " | " +  __instance.itemBattery.batteryLifeInt + " | Drain " + __instance.batteryDrain);
        }
    }

    [HarmonyPatch("Update")]
    [HarmonyPrefix]
    public static void OnUpdate(ItemGun __instance) {
        if (__instance.name.Contains("Item Gun Handgun")) {
            if (__instance.batteryDrain != PluginConfig.GetDrainCharge(PluginConfig.HandgunMaxShots.Value)) {
                ConfigurableWeapons.Logger.LogWarning("Battery Drain is not matching | Expected: " + PluginConfig.GetDrainCharge(PluginConfig.HandgunMaxShots.Value) + " | Actual: " + __instance.batteryDrain);
            }
        }
    }
}