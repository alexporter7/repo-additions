using System;
using System.Reflection;
using AModLib.AssetBundles;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using RepoToast.Notification;
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

        Logger.LogInfo($"Completed loading RepoToast asset bundle");
        ToastNotification = BundleHelper.GetPrefabFromBundle(ToastAssetBundle, "ToastNotificationGO");
        Logger.LogInfo(ToastNotification.name);

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
        if (SemiFunc.InputDown(InputKey.Crouch))
            SpawnToast();
    }

    private void SpawnToast() {
        // GameObject gameUi = GameObject.Find("Game Manager");
        // var toast = gameUi.AddComponent<ToastUI>();
        var toast = gameObject.AddComponent<ToastUI>().SetNotificationStruct(Notifications.OnExtractUnlocked);


        Logger.LogInfo($"GO attached to toast: [{toast.gameObject.name}] | Active: [{toast.isActiveAndEnabled}]");

        // ToastNotification.hideFlags = HideFlags.HideAndDontSave;
        // ToastNotification.SetActive(true);
        // var notif = Instantiate(ToastNotification);
        // notif.transform.SetParent(gameUi.transform);
        // notif.hideFlags                           = HideFlags.HideAndDontSave;
        // Logger.LogInfo($"Flags: [{notif.hideFlags}] | Active: [{notif.activeSelf}] [{notif.activeInHierarchy}] | Transform: [{notif.transform.parent.name}] [{gameUi.transform.name}]");
        // var comp = gameUi.AddComponent<ToastUI>();
        // foreach(TextMeshProUGUI text in gameUi.GetComponent<ToastUI>().ToastNotification.GetComponentsInChildren<TextMeshProUGUI>())
        //     Logger.LogInfo($"Found TextMeshProUGUI text is [{text.text}]");
        //comp.transform.SetParent(gameUi.transform);
        //Logger.LogInfo($"ToastUI Status: [{comp.isActiveAndEnabled}]");
    }

}