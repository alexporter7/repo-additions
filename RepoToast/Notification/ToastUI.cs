using System;
using TMPro;
using UnityEngine;
using Timer = AModLib.Api.Timers.Timer;

namespace RepoToast.Notification;

public class ToastUI : MonoBehaviour {

    private       string               Title;      
    private       NotificationType     ToastType;  
    private       Func<string, string> Description;
    
    private       Timer                Timer;
    public        GameObject           ToastNotification;
    public static ToastUI              Instance;

    private void Awake() {
        RepoToast.Logger.LogInfo($"ToastUI for Notification Type {ToastType} has awoken");

        ToastNotification = Instantiate(RepoToast.Instance.ToastNotification);
        ToastNotification.transform.SetParent(transform);
        
        Timer = gameObject.AddComponent<Timer>();
        Timer.SetOnComplete(() => Destroy(this));
        
        hideFlags = HideFlags.HideAndDontSave;
        Instance  = this;
    }

    private void Start() {
        RepoToast.Logger.LogInfo($"Toast Type: [{ToastType}] | Title: [{Title}] | Description: [{Description.Invoke("Test Player")}");
        foreach (var textComponent in ToastNotification.GetComponentsInChildren<TextMeshProUGUI>()) {
            if (textComponent.name == "Header") textComponent.text = Title;
            else textComponent.text = Description.Invoke(SemiFunc.PlayerGetName(SemiFunc.PlayerAvatarLocal()));
        }
        RepoToast.Logger.LogInfo($"Starting timer for Notification Type {ToastType}");
        Timer.StartTimer();
    }

    private void Update() {
    }

    private void OnDestroy() {
        Destroy(ToastNotification);
        Destroy(Timer);
        RepoToast.Logger.LogInfo($"ToastUI for Notification Type {ToastType} has been destroyed");
    }

    public ToastUI SetNotificationStruct(NotificationStruct notificationStruct) {
        Title       = notificationStruct.Title;
        ToastType   = notificationStruct.Type;
        Description = notificationStruct.Description;
        return this;
    }
}