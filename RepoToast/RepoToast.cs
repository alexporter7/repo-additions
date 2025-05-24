using System;
using System.Reflection;
using AModLib.AssetBundles;
using BepInEx;
using BepInEx.Logging;
using ExitGames.Client.Photon;
using HarmonyLib;
using REPOLib.Modules;
using RepoToast.Notification;
using RepoToast.Plugin;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace RepoToast;

[BepInPlugin("com.github.alexp777.RepoToast", "RepoToast", "0.1.0")]
[BepInDependency("nickklmao.menulib", "2.4.1")]
public class RepoToast : BaseUnityPlugin {

    internal static     RepoToast       Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger   => Instance._logger;
    private             ManualLogSource _logger  => base.Logger;
    internal            Harmony?        Harmony  { get; set; }

    public string      PluginBasePath = Assembly.GetExecutingAssembly().Location;
    public AssetBundle ToastAssetBundle;
    public GameObject  ToastNotification;

    private void Awake() {
        Instance = this;

        // Prevent the plugin from being deleted
        this.gameObject.transform.parent = null;
        this.gameObject.hideFlags        = HideFlags.HideAndDontSave;

        Logger.LogInfo("Loading Plugin Config");
        PluginConfig.LoadConfigs(Instance.Config);

        Logger.LogInfo($"Loading RepoToast asset bundle using PluginBasePath [{PluginBasePath}]");
        ToastAssetBundle = AssetBundle.LoadFromFile(BundleHelper.GetAssetBundlePath("repotoast", PluginBasePath));

        Logger.LogInfo("Completed loading RepoToast asset bundle");
        ToastNotification = BundleHelper.GetPrefabFromBundle(ToastAssetBundle, "ToastNotificationGO");
        
        Logger.LogInfo("Registering Notification Structs");
        Notifications.RegisterNotifications();
        
        Logger.LogInfo("Registering Notification Actions");
        Actions.Actions.RegisterActions();

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

}