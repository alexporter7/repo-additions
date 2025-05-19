using AModLib.Api.State;
using UnityEngine.InputSystem.LowLevel;

namespace RepoToast.Notification;

public class NotificationStates {

    public enum NotificationState {

        Inactive,
        Active

    }

    public static ObjectState<NotificationState, ToastNotification> Inactive =
        new ObjectState<NotificationState, ToastNotification>(NotificationState.Inactive)
            .AddValidNextState(NotificationState.Active)
            .SetStateSupplier(toast => NotificationState.Active);

    public static ObjectState<NotificationState, ToastNotification> Active =
        new ObjectState<NotificationState, ToastNotification>(NotificationState.Active)
            .AddValidNextState(NotificationState.Inactive)
            .SetStateSupplier(toast => NotificationState.Inactive)
            .SetOnStateEnter(toast => toast.ResetActiveTimer())
            .SetOnStateExit(toast => toast.DeactiveNotification());

}