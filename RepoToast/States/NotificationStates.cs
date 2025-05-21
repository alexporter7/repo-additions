using AModLib.Api.State;
using RepoToast.Notification;

namespace RepoToast.States;

public static class NotificationStates {

    public enum NotificationState {

        Queued,
        Opening,
        Active,
        Closing

    }

    public static ObjectState<NotificationState, ToastUI> Queued
        = new ObjectState<NotificationState, ToastUI>(NotificationState.Queued);
    
    public static ObjectState<NotificationState, ToastUI> Opening 
        = new ObjectState<NotificationState, ToastUI>(NotificationState.Opening);
    
    public static ObjectState<NotificationState, ToastUI> Active 
        = new ObjectState<NotificationState, ToastUI>(NotificationState.Active);
    
    public static ObjectState<NotificationState, ToastUI> Closing 
        = new ObjectState<NotificationState, ToastUI>(NotificationState.Closing);

}