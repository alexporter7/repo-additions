using System;
using System.Collections.Generic;
using System.Linq;
using static RepoToast.PluginLogHelper;

namespace RepoToast.Notification;

public class NotificationManager {
    
    private List<ToastNotification>                      ActiveNotifications = [];

    public void RequestNotification(NotificationStruct notificationStruct) {
        LogNotificationRequest(notificationStruct.Type);
        ActiveNotifications.Add(new ToastNotification(notificationStruct).RequestNotification(this));
    }

    public void TickNotificationManager(float time) {
        if (ActiveNotifications.Count == 0)
            return;
        foreach(ToastNotification notification in ActiveNotifications.ToList())
            notification.DecrementTimer(time);
    }

    public void RemoveActiveNotification(ToastNotification notification) {
        ActiveNotifications.Remove(notification);
    }

}