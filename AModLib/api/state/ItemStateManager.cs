using System;
using System.Collections.Generic;
using MonoMod.Utils;
using UnityEngine;

namespace AModLib.api.state;

public class ItemStateManager<T> {

    private readonly Dictionary<T, ItemState<T>> ItemStates;
    public           T                           CurrentState;
    public           T                           PreviousState;
    public           Component                   ComponentInstance;


    public ItemStateManager<T> RegisterState(T state, ItemState<T> itemState) {
        ItemStates.Add(state, itemState);
        return this;
    }

    public bool CanEnterState(T state) {
        return ItemStates[state].IsValidStateTransition(state);
    }

    public bool RequestState(T state) {
        if (!CanEnterState(state))
            return LogInvalidStateTransition(state);

        PreviousState = CurrentState;
        CurrentState  = state;

        ItemStates[PreviousState].OnStateExit?.Invoke(ComponentInstance);
        ItemStates[CurrentState].OnStateEnter?.Invoke(ComponentInstance);

        return LogStateTransition();
    }

    public bool RequestNextState() {
        T requestedState = ItemStates[CurrentState].StateSupplier.Invoke(ComponentInstance);
        if (!CanEnterState(requestedState))
            return LogInvalidStateTransition(requestedState);
        
        RequestState(requestedState);
        return true;
    }

    public void ExecuteStateTick() {
        ItemStates[CurrentState].OnStateTick?.Invoke(ComponentInstance);
    }

    private bool LogInvalidStateTransition(T state) {
        AModLib.Logger.LogWarning(
            $"Component {ComponentInstance.name} tried to enter state {state} while currently in state {CurrentState}.");
        return false;
    }

    private bool LogStateTransition() {
        AModLib.Logger.LogDebug(
            $"Component {ComponentInstance.name} has changed from state {PreviousState} to {CurrentState}");
        return true;
    }

}