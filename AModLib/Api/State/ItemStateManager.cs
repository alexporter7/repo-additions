using System.Collections.Generic;
using UnityEngine;

namespace AModLib.Api.State;

public class ItemStateManager<T> {

    private readonly Dictionary<T, ItemState<T>> ItemStates;
    public           T                           CurrentState;
    public           T                           PreviousState;
    public           Component                   ComponentInstance;


    public ItemStateManager<T> RegisterState(T state, ItemState<T> itemState) {
        ItemStates.Add(state, itemState);
        return this;
    }

    public ItemStateManager<T> RegisterComponentInstance(Component component) {
        ComponentInstance = component;
        return this;
    }

    public bool CanEnterState(T state) {
        return ItemStates[CurrentState].IsValidStateTransition(state);
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
            $"Component {ComponentInstance.name} tried to enter State {state} while currently in State {CurrentState}.");
        return false;
    }

    private bool LogStateTransition() {
        AModLib.Logger.LogDebug(
            $"Component {ComponentInstance.name} has changed from State {PreviousState} to {CurrentState}");
        return true;
    }

}