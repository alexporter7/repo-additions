using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace RepoToast;

[BepInPlugin("com.github.alexp777.RepoToast", "RepoToast", "0.1.0")]
[BepInDependency("nickklmao.menulib", "2.4.1")]
public class RepoToast : BaseUnityPlugin {

    internal static     RepoToast       Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger   => Instance._logger;
    private             ManualLogSource _logger  => base.Logger;
    internal            Harmony?        Harmony  { get; set; }

    private void Awake() {
        Instance = this;

        // Prevent the plugin from being deleted
        this.gameObject.transform.parent = null;
        this.gameObject.hideFlags        = HideFlags.HideAndDontSave;
        
        PluginConfig.LoadConfigs(Config);

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