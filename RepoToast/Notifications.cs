using System;
using System.Collections.Generic;
using AModLib.Api.Network;
using RepoToast.Notification;
using static RepoToast.Notification.ContextComponent;

namespace RepoToast;

public static class Notifications {

    public static readonly Dictionary<NotificationType, NotificationStruct> NotificationStructs = [];

    public static void RegisterNotifications() {
        /* Extraction Notifications */
        RegisterNotification(NotificationType.OnExtractionUnlocked, "Extraction Unlocked",
            (ctx) => $"{ctx.GetProp(PlayerName)} has just " +
                     $"unlocked an extraction point!");
        RegisterNotification(NotificationType.OnExtractionCompleted, "Extraction Completed",
            (ctx) => $"{ctx.GetProp(PlayerName)} has just " +
                     $"completed an extraction by placing {ctx.GetProp(ValuableName)}!");

        /* Player Notifications */
        RegisterNotification(NotificationType.OnPlayerDeath, "A Player Has Died",
            (ctx) => $"{ctx.GetProp(PlayerName)} has just died " +
                     $"from {ctx.GetProp(PlayerKilledBy)}");
        RegisterNotification(NotificationType.OnPlayerRevive, "A Player Has Been Revived",
            (ctx) => $"{ctx.GetProp(PlayerName)} has just been " +
                     $"brought back from the dead by {ctx.GetProp(PlayersInvolved)}");

        /* Valuable Notifications */
        RegisterNotification(NotificationType.OnValuableDestroyed, "A Valuable Has Been Destroyed",
            (ctx) => $"{ctx.GetProp(PlayerName)} has just destroyed " +
                     $"{ctx.GetProp(ValuableName)} which was worth ${ctx.GetProp(ValuableInitialValue)}. " +
                     $"Make sure you give them what they deserve :)");

        /* Monster Notifications */
        RegisterNotification(NotificationType.OnMonsterKilled, "A Monster Has Just Been Eliminated",
            (ctx) => $"{ctx.GetProp(PlayerName)} has just eliminated " +
                     $"{ctx.GetProp(MonsterName)}");
        RegisterNotification(NotificationType.OnMonsterSpawn, "A Monster Has Just Been Spawned",
            (ctx) => $"{ctx.GetProp(MonsterName)} has just spawned! " +
                     $"{ctx.GetProp(MonsterName)} has {ctx.GetProp(MonsterHealth)} health.");
    }

    public static void RegisterNotification(NotificationType type, string title,
                                            Func<NetworkEventContext<ContextComponent>, string> description) {
        NotificationStructs.Add(type, new NotificationStruct(type, title, description));
        RepoToast.Logger.LogInfo($"Registered new notification type [{type}]");
    }

    public static NotificationStruct GetNotification(NotificationType notificationType) {
        return NotificationStructs[notificationType];
    }

}