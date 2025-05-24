using System;
using AModLib.Api.Network;
using RepoToast.Notification;

namespace RepoToast.Actions;

public static class Actions {

    public static void RegisterActions() {
        Notifications.NotificationStructs[NotificationType.OnValuableDestroyed]
                     .SetAction((ctx) => {
                         PlayerAvatar player = SemiFunc
                             .PlayerGetFromName(ctx.GetProp<string>(ContextComponent.PlayerName));
                         
                         player.ChatMessageSend($"My mom calls me special because I drop things worth " +
                                                $"{ctx.GetProp(ContextComponent.ValuableInitialValue)} dollars", 
                             false);});
        
    }

}