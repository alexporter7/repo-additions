using System;
using System.Collections.Generic;

namespace AModLib.Api.State;

public class ObjectState<T, K>(T state) {

    public readonly T          State = state;
    public          Action<K>  OnStateEnter;
    public          Action<K>  OnStateExit;
    public          Action<K>  OnStateTick;
    public          Func<K, T> StateSupplier;
    private         List<T> ValidNextStates = [];

    public ObjectState<T, K> AddValidNextState(T state) {
        ValidNextStates.Add(state);
        return this;
    }

    public ObjectState<T, K> SetOnStateEnter(Action<K> action) {
        OnStateEnter = action;
        return this;
    }

    public ObjectState<T, K> SetOnStateExit(Action<K> action) {
        OnStateExit = action;
        return this;
    }

    public ObjectState<T, K> SetOnStateTick(Action<K> action) {
        OnStateTick = action;
        return this;
    }

    public ObjectState<T, K> SetStateSupplier(Func<K, T> action) {
        StateSupplier = action;
        return this;
    }

    public bool IsValidStateTransition(T state) {
        return ValidNextStates.Contains(state);
    }

}