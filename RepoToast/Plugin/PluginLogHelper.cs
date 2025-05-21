using System;
using RepoToast.Notification;

namespace RepoToast.Plugin;

public static class PluginLogHelper {
    public static void LogNotificationRequest(NotificationType key) {
        RepoToast.Logger.LogInfo($"[{SemiFunc.PlayerGetName(PlayerAvatar.instance)}] " +
                                 $"has requested [{Enum.GetName(typeof(NotificationType), key)}]");
    }

}