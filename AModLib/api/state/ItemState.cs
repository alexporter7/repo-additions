using System;
using System.Collections.Generic;
using UnityEngine;
using static AModLib.api.delegates.ComponentDelegates;

namespace AModLib.api.state;

public class ItemState<T>(T state) {

    public delegate T GetNextState(Component component);
    
    public           T              State = state;
    public           UpdateProperty OnStateEnter;
    public           UpdateProperty OnStateExit;
    public           UpdateProperty OnStateTick;
    public           GetNextState   StateSupplier;
    private readonly HashSet<T>     ValidNextStates = [];

    public ItemState<T> AddValidNextState(T state) {
        ValidNextStates.Add(state);
        return this;
    }

    public ItemState<T> SetOnStateEnter(UpdateProperty action) {
        OnStateEnter = action;
        return this;
    }

    public ItemState<T> SetOnStateExit(UpdateProperty action) {
        OnStateExit = action;
        return this;
    }

    public ItemState<T> SetOnStateTick(UpdateProperty action) {
        OnStateTick = action;
        return this;
    }

    public ItemState<T> SetStateSupplier(GetNextState action) {
        StateSupplier = action;
        return this;
    }

    public bool IsValidStateTransition(T state) {
        return ValidNextStates.Contains(state);
    }

}