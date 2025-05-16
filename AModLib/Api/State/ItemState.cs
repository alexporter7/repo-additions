using System;
using System.Collections.Generic;
using UnityEngine;

namespace AModLib.Api.State;

public class ItemState<T>(T state) {

    public readonly  T                  State = state;
    public           Action<Component>  OnStateEnter;
    public           Action<Component>  OnStateExit;
    public           Action<Component>  OnStateTick;
    public           Func<Component, T> StateSupplier;
    private readonly HashSet<T>         ValidNextStates = [];

    public ItemState<T> AddValidNextState(T state) {
        ValidNextStates.Add(state);
        return this;
    }

    public ItemState<T> SetOnStateEnter(Action<Component> action) {
        OnStateEnter = action;
        return this;
    }

    public ItemState<T> SetOnStateExit(Action<Component> action) {
        OnStateExit = action;
        return this;
    }

    public ItemState<T> SetOnStateTick(Action<Component> action) {
        OnStateTick = action;
        return this;
    }

    public ItemState<T> SetStateSupplier(Func<Component, T> action) {
        StateSupplier = action;
        return this;
    }

    public bool IsValidStateTransition(T state) {
        return ValidNextStates.Contains(state);
    }

}