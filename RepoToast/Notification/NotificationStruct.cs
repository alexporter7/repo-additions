using System;

namespace RepoToast.Notification;

public class NotificationStruct(NotificationType type, string title, Func<NotificationContext, string> description) {

    public string                            Title       = title;
    public NotificationType                  Type        = type;
    public Func<NotificationContext, string> Description = description;

}