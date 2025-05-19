using System;
using AModLib.Api.Timers;
using UnityEngine;

namespace RepoToast.Notification;

public class ToastUI : MonoBehaviour {

    private string               Title;
    private NotificationType     ToastType;
    private Func<string, string> Description;
    private Timer                Timer;
    
    private void Awake() {
        RepoToast.Logger.LogInfo($"ToastUI for Notification Type {ToastType} has awoken");
        Timer = gameObject.AddComponent<Timer>();
        Timer.SetOnComplete(() => Destroy(this));
    }

    private void Start() {
        Timer.StartTimer();
    }

    private void Update() {
        throw new NotImplementedException();
    }

    private void OnDestroy() {
        RepoToast.Logger.LogInfo($"ToastUI for Notification Type {ToastType} has been destroyed");
    }

}