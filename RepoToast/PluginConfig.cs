using System;
using BepInEx.Configuration;
using RepoToast.Notification;

namespace RepoToast;

public static class PluginConfig {

    private static ConfigFile _config = null!;
    
    public static void LoadConfigs(ConfigFile config) {
        _config = config;
        LoadGlobalConfigs();
        LoadNotificationTypeConfigs();
    }

    private static void LoadGlobalConfigs() {
        _config.Bind(
            "Global Settings",
            "Enable RepoToast",
            false,
            "Enable or Disable all notification types");
        _config.Bind(
            "Global Settings",
            "Notification Time",
            3f,
            "How long will a toast notification stay on the screen");
    }

    private static void LoadNotificationTypeConfigs() {
        foreach (NotificationType type in Enum.GetValues(typeof(NotificationType)))
            _config.Bind(
                "Notification Type",
                $"Enable {Enum.GetName(typeof(NotificationType), type)} Notifications",
                true,
                $"Enables or disables all {Enum.GetName(typeof(NotificationType), type)} notifications");
    }

}