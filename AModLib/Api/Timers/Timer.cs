using System;
using UnityEngine;

namespace AModLib.Api.Timers;

public class Timer : MonoBehaviour {

    private float  TimerDefault = 1f;
    private float  TimerValue   = 1f;
    private bool   Ticking      = false;
    private Action OnComplete;

    private void Awake() {
        AModLib.Logger.LogDebug("Timer component has been instantiated");
    }

    private void Reset() {
        AModLib.Logger.LogDebug("Timer component has been reset");
    }

    private void Start() {
        if (!Mathf.Approximately(TimerValue, TimerDefault))
            TimerValue = TimerDefault;
        AModLib.Logger.LogDebug("Timer component has been started");
    }

    private void Update() {
        if (!Ticking)
            return;
        TimerValue -= Time.deltaTime;

        if (TimerValue <= 0f)
            TimerDone();
    }

    public void SetOnComplete(Action onComplete) {
        OnComplete = onComplete;
    }

    private void TimerDone() {
        StopTimer();
        OnComplete?.Invoke();
    }

    public void StartTimer() {
        Ticking = true;
    }

    public void StopTimer() {
        Ticking = false;
    }

    public void ResetTimer() {
        TimerValue = TimerDefault;
    }

}