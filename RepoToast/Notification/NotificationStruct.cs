using System;

namespace RepoToast.Notification;

public class NotificationStruct(NotificationType type, string title, Func<string, string> description) {

    public string               Title       = title;
    public float                ActiveTimer = 3f;
    public NotificationType     Type        = type;
    public Func<string, string> Description = description;

}