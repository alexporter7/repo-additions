using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AModLib.Components;

public class InteractionTracker : MonoBehaviour {

    public enum InteractionType {

        Player,
        Monster,
        Other

    }

    private Dictionary<InteractionType, List<GameObject>> Interactions = [];

    private void Awake() {
        AModLib.Logger.LogDebug($"InteractionTracker has been attached to [{gameObject.name}]");
    }

    private void Start() {
        AModLib.Logger.LogDebug($"InteractionTracker for [{gameObject.name}] has started");
    }

    private void Reset() {
        AModLib.Logger.LogDebug($"InteractionTracker for [{gameObject.name}] has restarted");
        Interactions = [];
    }

    private void Update() {
        
    }

    private void OnDestroy() {
        AModLib.Logger.LogDebug($"InteractionTracker for [{gameObject.name}] has been destroyed");
    }

}