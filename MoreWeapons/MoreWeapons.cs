using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace MoreWeapons;

[BepInPlugin("com.github.alexp777.MoreWeapons", "MoreWeapons", "1.0")]
[BepInDependency("REPOLib", "2.1.0")]
public class MoreWeapons : BaseUnityPlugin {
    
    internal static     MoreWeapons     Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger   => Instance._logger;
    private             ManualLogSource _logger  => base.Logger;
    internal            Harmony?        Harmony  { get; set; }

    private void Awake() {
        Instance = this;
        
        //PluginBundleLoader.LoadBundles();
        PluginBundleLoader.LoadAssetBundle();

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