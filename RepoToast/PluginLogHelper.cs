using System;
using RepoToast.Notification;

namespace RepoToast;

public static class PluginLogHelper {

    public static void LogNotificationRegistration(NotificationType key) {
        RepoToast.Logger.LogInfo($"Registered Toast Notification [{Enum.GetName(typeof(NotificationType), key)}]");
    }

    public static void LogNotificationRequest(NotificationType key) {
        RepoToast.Logger.LogInfo($"[{SemiFunc.PlayerGetName(PlayerAvatar.instance)}] " +
                                 $"has requested [{Enum.GetName(typeof(NotificationType), key)}]");
    }

}