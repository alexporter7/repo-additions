using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using AModLib.Api.Network;
using BepInEx;
using BepInEx.Configuration;
using ExitGames.Client.Photon;
using OdinSerializer;
using REPOLib.Modules;
using RepoToast.Notification;

namespace RepoToast.Plugin;

public static class PluginEvents {
    
    public static NetworkedEvent SpawnToastEvent = new("SpawnToastEvent", HandleSpawnToastEvent);

    public static void HandleSpawnToastEvent(EventData eventData) {
        NetworkEventContext<ContextComponent> ctx = NetworkDeserializer
            .DeserializeEventData<NetworkEventContext<ContextComponent>>(eventData);
        RepoToast.Instance.gameObject.AddComponent<ToastUI>()
                 .SetContext(ctx);
        LogEventCall(ctx);
    }

    private static void LogEventCall(NetworkEventContext<ContextComponent> eventContext) {
        RepoToast.Logger.LogInfo($"Event [{eventContext
                                           .GetProp<NotificationStruct>(ContextComponent.NotificationStruct)
                                           .Type}] has been fired");
    }

    
    public static void ConfigFileReloadedEvent(object sender, EventArgs eventArgs) {
        PluginConfig.LoadConfigs(RepoToast.Instance.Config);
        RepoToast.Logger.LogInfo("Configuration File has been reloaded");
    }

    //TODO: make this work
    public static void ConfigFileSettingChangeEvent(object sender, SettingChangedEventArgs eventArgs) {
        string configKey  = eventArgs.ChangedSetting.Definition.Key;
        RepoToast.Logger.LogInfo($"Configuration Setting [{configKey}] was changed");
    }

}