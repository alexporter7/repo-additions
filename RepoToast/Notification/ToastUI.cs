using System;
using System.Collections.Generic;
using AModLib.Api.Network;
using AModLib.Utils;
using TMPro;
using UnityEngine;
using Timer = AModLib.Components.Timer;

namespace RepoToast.Notification;

public class ToastUI : MonoBehaviour {

    private string                                              Title;
    private NotificationType                                    ToastType;
    private Func<NetworkEventContext<ContextComponent>, string> Description;
    private NetworkEventContext<ContextComponent>               NotificationContext;
    private Action<NetworkEventContext<ContextComponent>>       NotificationAction;

    private       Timer      Timer;
    public        GameObject ToastNotification;
    public static ToastUI    Instance;

    private void Awake() {
        RepoToast.Logger.LogInfo($"ToastUI for Notification Type {ToastType} has awoken");

        ToastNotification = Instantiate(RepoToast.Instance.ToastNotification);
        ToastNotification.transform.SetParent(transform);

        Timer = gameObject.AddComponent<Timer>();
        Timer.SetOnComplete(() => Destroy(this));
        Timer.SetTimerDefault(ConfigUtil.GetConfigOptionValue<float>(
            RepoToast.Instance.Config, "Global Settings", "Notification Time"));

        hideFlags = HideFlags.HideAndDontSave;
        Instance  = this;
    }

    private void Start() {
        RepoToast.Logger.LogInfo(
            $"Toast Type: [{ToastType}] | Title: [{Title}] | Description: [{Description.Invoke(NotificationContext)}");

        foreach (var textComponent in ToastNotification.GetComponentsInChildren<TextMeshProUGUI>()) {
            if (textComponent.name == "Header") textComponent.text = Title;
            else textComponent.text                                = Description.Invoke(NotificationContext);
        }

        var yOffset = ConfigUtil.GetConfigOptionValue<int>(
            RepoToast.Instance.Config,
            "Notification UI",
            "Toast Notification Y offset");

        RectTransform toastRectTransform = ToastNotification.GetComponent<RectTransform>();
        Vector3       currentPostion     = toastRectTransform.position;
        toastRectTransform.position.Set(currentPostion.x, currentPostion.y - yOffset, currentPostion.z);

        RepoToast.Logger.LogInfo($"Starting timer for Notification Type {ToastType}");
        Timer.StartTimer();
        NotificationAction?.Invoke(NotificationContext);
    }

    private void Update() {
    }

    private void OnDestroy() {
        Destroy(ToastNotification);
        Destroy(Timer);
        RepoToast.Logger.LogInfo($"ToastUI for Notification Type {ToastType} has been destroyed");
    }

    public void SetContext(NetworkEventContext<ContextComponent> ctx) {
        NotificationStruct notificationStruct = ctx.GetProp<NotificationStruct>(ContextComponent.NotificationStruct);
        Title               = notificationStruct.Title;
        ToastType           = notificationStruct.Type;
        Description         = notificationStruct.Description;
        NotificationAction  = notificationStruct.NotificationAction;
        NotificationContext = ctx;
    }

}