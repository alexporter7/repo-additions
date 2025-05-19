using System;
using AModLib.Api.State;
using BepInEx.Configuration;
using MenuLib;
using MenuLib.MonoBehaviors;
using UnityEngine;
using static RepoToast.Notification.NotificationStates.NotificationState;

namespace RepoToast.Notification;

public class ToastNotification {

    private string               Title;
    private NotificationType     Type;
    private Func<string, string> Description;
    private float                ActiveTimer = 3f;
    private NotificationManager  Manager;
    private REPOPopupPage        Toast;

    private ObjectStateManager<NotificationStates.NotificationState, ToastNotification> StateManager = new();

    public ToastNotification(NotificationStruct notificationStruct) {
        Title       = notificationStruct.Title;
        Type        = notificationStruct.Type;
        Description = notificationStruct.Description;
        StateManager
                .RegisterState(Inactive, NotificationStates.Inactive)
                .RegisterState(Active, NotificationStates.Active)
                .SetDefaultState(Inactive)
                .RegisterObjectInstance(this)
                .Register();
        RepoToast.Logger.LogInfo($"Created new Toast Notification [{Type}]");
    }

    public ToastNotification RequestNotification(NotificationManager manager) {
        StateManager.RequestNextState();
        Manager = manager;
        Toast = MenuAPI.CreateREPOPopupPage(
            Title,
            REPOPopupPage.PresetSide.Right,
            false,
            false);
        Toast.OpenPage(true);
        return this;
    }

    public void ResetActiveTimer() {
        RepoToast.Instance.Config.TryGetEntry(
            "Global Settings",
            "Notification Time",
            out ConfigEntry<float> timerSetting);
        ActiveTimer = timerSetting.Value;
    }

    public void DeactiveNotification() {
        RepoToast.Logger.LogInfo($"Closing Toast Notification [{Type}]");
        Manager.RemoveActiveNotification(this);
        Toast.ClosePage(false);
    }

    public void DecrementTimer(float time) {
        if (Math.Max(0, ActiveTimer - time) == 0)
            StateManager.RequestNextState();
        ActiveTimer -= time;
    }

    public override string ToString() => Title;

}