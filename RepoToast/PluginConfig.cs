using System;
using BepInEx.Configuration;

namespace RepoToast;

public static class PluginConfig {

    private static ConfigFile Config;
    
    public static void LoadConfigs(ConfigFile config) {
        Config = config;
        LoadGlobalConfigs();
        LoadNotificationTypeConfigs();
    }

    private static void LoadGlobalConfigs() {
        Config.Bind(
            "Global Settings",
            "Enable RepoToast",
            false,
            "Enable or Disable all notification types");
    }

    private static void LoadNotificationTypeConfigs() {
        foreach (NotificationType type in Enum.GetValues(typeof(NotificationType)))
            Config.Bind(
                "Notification Type",
                $"Enable {Enum.GetName(typeof(NotificationType), type)} Notifications",
                true,
                $"Enables or disables all {Enum.GetName(typeof(NotificationType), type)} notifications");
    }

}