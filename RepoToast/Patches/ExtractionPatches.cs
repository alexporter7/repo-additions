using System.Collections.Generic;
using System.Linq;
using AModLib.Api.Network;
using AModLib.Utils;
using ExitGames.Client.Photon;
using HarmonyLib;
using RepoToast.Components;
using RepoToast.Notification;
using RepoToast.Plugin;
using Unity.VisualScripting;
using UnityEngine;

namespace RepoToast.Patches;

[HarmonyPatch(typeof(ExtractionPoint))]
internal class ExtractionPatches {

    [HarmonyPostfix]
    [HarmonyPatch("Start")]
    private static void OnExtractionPointStart(ExtractionPoint __instance) {
        __instance.buttonGrabObject.AddComponent<InteractionHandler>();
    }

    [HarmonyPrefix]
    [HarmonyPatch("ActivateTheFirstExtractionPointAutomaticallyWhenAPlayerLeaveTruck")]
    private static void OnFirstExtractionCompletion(ExtractionPoint __instance) {
        if (__instance.isShop)
            return;
        
        PlayerAvatar player = DistanceUtil.GetClosestPlayerToPos(
            GameDirector.instance.PlayerList,
            __instance.ramp.position);
        
        InteractionHandler handler = __instance.GetComponentInChildren<InteractionHandler>();
        handler.AddInteraction(player);
    }

    [HarmonyPostfix]
    [HarmonyPatch("StateSet")]
    private static void OnStateSet(ExtractionPoint __instance, ExtractionPoint.State newState) {
        RepoToast.Logger.LogDebug($"Extraction point has entered state [{newState}]");
        if (__instance.isShop)
            return;
        
        InteractionHandler interactionHandler = __instance.GetComponentInChildren<InteractionHandler>();
        NetworkEventContext<ContextComponent> ctx =
            new NetworkEventContext<ContextComponent>();

        if (newState == ExtractionPoint.State.Active) {
            ctx.AddContextComponent(ContextComponent.NotificationStruct,
                   Notifications.GetNotification(NotificationType.OnExtractionUnlocked))
               .AddContextComponent(ContextComponent.PlayerName, 
                   SemiFunc.PlayerGetName(interactionHandler.LastInteractionPlayer));
        }
        else if (newState == ExtractionPoint.State.Success) {
            ValuableObject lastValuableAdded = 
                RoundDirector.instance.dollarHaulList.Last().GetComponent<ValuableObject>();
            
            RepoToast.Logger.LogDebug($"Last Object added to extraction point was [{lastValuableAdded.name}]");
            PlayerAvatar lastPlayer = lastValuableAdded.GetComponent<InteractionHandler>().LastInteractionPlayer;
            
            
            ctx.AddContextComponent(ContextComponent.NotificationStruct,
                   Notifications.GetNotification(NotificationType.OnExtractionCompleted))
               .AddContextComponent(ContextComponent.PlayerName, 
                   lastPlayer.playerName)
               .AddContextComponent(ContextComponent.ValuableName, 
                   lastValuableAdded.name.Replace("(Clone)", ""));
        }
        else {
            return;
        }
        PluginEvents.SpawnToastEvent.RaiseEvent(ctx.Serialize(),
            REPOLib.Modules.NetworkingEvents.RaiseAll,
            SendOptions.SendReliable);
    }

}

[HarmonyPatch(typeof(StaticGrabObject))]
internal class StaticGrabObjectPatches {

    [HarmonyPostfix]
    [HarmonyPatch("Start")]
    private static void OnStartPostfix(StaticGrabObject __instance) {
        //__instance.AddComponent<InteractionHandler>();
    }

    [HarmonyPostfix]
    [HarmonyPatch("GrabStarted")]
    private static void OnGrabStartedPostfix(StaticGrabObject __instance, PhysGrabber player) {
        __instance.GetComponent<InteractionHandler>().AddInteraction(player.playerAvatar);
    }

}