using System;
using System.Collections.Generic;
using BepInEx.Configuration;
using RepoToast.Notification;

namespace RepoToast.Plugin;

public static class PluginConfig {

    private static ConfigFile                              _config       = null!;

    public static void LoadConfigs(ConfigFile config) {
        _config = config;
        LoadGlobalConfigs();
        LoadNotificationUIConfigs();
        LoadNotificationTypeConfigs();

        config.ConfigReloaded += PluginEvents.ConfigFileReloadedEvent;
        config.SettingChanged += PluginEvents.ConfigFileSettingChangeEvent;
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

    private static void LoadNotificationUIConfigs() {
        _config.Bind(
            "Notification UI",
            "Toast Notification Y offset",
            25,
            "How much additional offset starting from the top of the screen the notification will appear");
        //TODO: x start pos
        
        //TODO: y start pos
        
        //TODO: height
        
        //TODO: width
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