using System;
using System.Collections.Generic;
using System.Linq;

namespace AModLib.Api.State;

public class ObjectStateManager<T, K> {

    private readonly Dictionary<T, ObjectState<T, K>> ObjectStates = new();
    public           T                                CurrentState;
    public           T                                PreviousState;
    public           K                                ObjectInstance;
    
    public ObjectStateManager<T, K> RegisterState(T state, ObjectState<T, K> objectState) {
        ObjectStates.Add(state, objectState);
        return this;
    }

    public ObjectStateManager<T, K> RegisterObjectInstance(K objectInstance) {
        ObjectInstance = objectInstance;
        return this;
    }

    public ObjectStateManager<T, K> SetDefaultState(T defaultState) {
        CurrentState  = defaultState;
        PreviousState = defaultState;
        return this;
    }

    public ObjectStateManager<T, K> Register() {
        if (CurrentState == null)
            AModLib.Logger.LogInfo($"State Manager for {typeof(T)} has no default state");
        return this;
    }

    public bool CanEnterState(T state) {
        return ObjectStates[CurrentState].IsValidStateTransition(state);
    }

    public bool RequestState(T state) {
        if (!CanEnterState(state))
            return LogInvalidStateTransition(state);

        PreviousState = CurrentState;
        CurrentState  = state;

        ObjectStates[PreviousState].OnStateExit?.Invoke(ObjectInstance);
        ObjectStates[CurrentState].OnStateEnter?.Invoke(ObjectInstance);

        return LogStateTransition();
    }

    public bool RequestNextState() {
        T requestedState = ObjectStates[CurrentState].StateSupplier.Invoke(ObjectInstance);
        if (!CanEnterState(requestedState))
            return LogInvalidStateTransition(requestedState);
        
        RequestState(requestedState);
        return true;
    }

    public void ExecuteStateTick() {
        ObjectStates[CurrentState].OnStateTick?.Invoke(ObjectInstance);
    }

    private bool LogInvalidStateTransition(T state) {
        AModLib.Logger.LogWarning(
            $"Object {ObjectInstance.ToString()} tried to enter State {state} while currently in State {CurrentState}.");
        return false;
    }

    private bool LogStateTransition() {
        AModLib.Logger.LogDebug(
            $"Object {ObjectInstance.ToString()} has changed from State {PreviousState} to {CurrentState}");
        return true;
    }

    private void LogStateManagerRegistration() {
        AModLib.Logger.LogInfo(
            $"State Manager registered for [{typeof(T)}] | Current State: {CurrentState}");
    }

}