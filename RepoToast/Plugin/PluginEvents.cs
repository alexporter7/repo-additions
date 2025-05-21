using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ExitGames.Client.Photon;
using OdinSerializer;
using REPOLib.Modules;
using RepoToast.Notification;

namespace RepoToast.Plugin;

public static class PluginEvents {

    public static NetworkedEvent SpawnToastEvent = new("SpawnToastEvent", HandleSpawnToastEvent);

    public static void HandleSpawnToastEvent(EventData eventData) {
        RepoToast.Instance.gameObject.AddComponent<ToastUI>()
                 .SetContext(DeserializeEventData<NotificationContext>(eventData));
        LogEventCall(eventData);
    }

    private static T DeserializeEventData<T>(EventData eventData) {
        return SerializationUtility.DeserializeValue<T>((byte[])eventData.CustomData, DataFormat.Binary);
    }

    private static void LogEventCall(EventData eventData) {
        RepoToast.Logger.LogInfo($"Event [{eventData.Code} has been fired");
    }

}