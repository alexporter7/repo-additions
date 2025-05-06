using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace ConfigurableWeapons;

[BepInPlugin("alexp777.ConfigurableWeapons", "ConfigurableWeapons", "0.1.0")]
[BepInDependency(REPOLib.MyPluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.HardDependency)]
public class ConfigurableWeapons : BaseUnityPlugin {
    
    internal static     ConfigurableWeapons Instance { get; private set; } = null!;
    internal new static ManualLogSource     Logger   => Instance._logger;
    private             ManualLogSource     _logger  => base.Logger;
    internal            Harmony?            Harmony  { get; set; }

    private void Awake() {
        Instance = this;
        
        Logger.LogInfo($"{Info.Metadata.GUID} v{Info.Metadata.Version} Loading Config");
        PluginConfig.Load(this.Config);
        
        // Prevent the plugin from being deleted
        this.gameObject.transform.parent = null;
        this.gameObject.hideFlags        = HideFlags.HideAndDontSave;

        Patch();

        Logger.LogInfo($"{Info.Metadata.GUID} v{Info.Metadata.Version} has loaded!");
    }

    internal void Patch() {
        Harmony ??= new Harmony(Info.Metadata.GUID);
        Harmony.PatchAll();
    }

    internal void Unpatch() {
        Harmony?.UnpatchSelf();
    }

    private void Update() {
        // Code that runs every frame goes here
    }
}