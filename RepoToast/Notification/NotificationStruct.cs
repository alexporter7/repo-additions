using System;
using System.Collections.Generic;
using AModLib.Api.Network;
using RepoToast.Actions;

namespace RepoToast.Notification;

public class NotificationStruct(
    NotificationType type,
    string title,
    Func<NetworkEventContext<ContextComponent>, string> description) {

    public string                                              Title       = title;
    public NotificationType                                    Type        = type;
    public Func<NetworkEventContext<ContextComponent>, string> Description = description;
    public Action<NetworkEventContext<ContextComponent>>       NotificationAction;

    public NotificationStruct SetAction(Action<NetworkEventContext<ContextComponent>> action) {
        NotificationAction = action;
        return this;
    }

}