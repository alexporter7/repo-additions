using System;
using AModLib.Api.Enums;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;

namespace AModLib;

[BepInPlugin("com.github.alexp777.AModLib", "AModLib", "1.0")]
[BepInDependency(REPOLib.MyPluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.HardDependency)]
[BepInDependency("nickklmao.menulib", "2.4.1")]

public class AModLib : BaseUnityPlugin {
    internal static     AModLib         Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger   => Instance._logger;
    private             ManualLogSource _logger  => base.Logger;
    internal            Harmony?        Harmony  { get; set; }
    internal            ClientType      ClientType;

    private void Awake() {
        
        gameObject.transform.parent = null;
        gameObject.hideFlags        = HideFlags.HideAndDontSave;

        Patch();
        
        Instance = this;
        Logger.LogInfo($"{Info.Metadata.GUID} v{Info.Metadata.Version} has loaded!");
        
    }

    private void Start() {
        ClientType = (!GameManager.Multiplayer())
            ? ClientType.Local
            : (PhotonNetwork.IsMasterClient)
                ? ClientType.Host
                : ClientType.Local;
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