using AModLib.Api.Network;
using ExitGames.Client.Photon;
using HarmonyLib;
using RepoToast.Components;
using RepoToast.Notification;
using RepoToast.Plugin;
using Unity.VisualScripting;

namespace RepoToast.Patches;

[HarmonyPatch(typeof(PhysGrabObject))]
internal class ValuablePatches {

    [HarmonyPostfix]
    [HarmonyPatch("Start")]
    private static void AttatchInteractionHandler(PhysGrabObject __instance) {
        if (!__instance.isValuable)
            return;

        ValuableObject valuable = __instance.GetComponent<ValuableObject>();
        __instance.AddComponent<InteractionHandler>();
    }

    [HarmonyPostfix]
    [HarmonyPatch("GrabStarted")]
    private static void OnGrabStarted(PhysGrabObject __instance, PhysGrabber player) {
        if (!__instance.isValuable)
            return;
        
        InteractionHandler interactionHandler = __instance.GetComponent<InteractionHandler>();
        if (__instance.grabbed)
            interactionHandler.AddInteraction(player.playerAvatar);
        
    }

    [HarmonyPostfix]
    [HarmonyPatch("DestroyPhysGrabObject")]
    private static void OnValuableDestroyed(PhysGrabObject __instance) {
        if (!__instance.isValuable || __instance.hasNeverBeenGrabbed)
            return;

        ValuableObject     valuable           = __instance.GetComponent<ValuableObject>();
        InteractionHandler interactionHandler = __instance.GetComponent<InteractionHandler>();

        if (interactionHandler.LastInteractionPlayer == null)
            return;

        NetworkEventContext<ContextComponent> context =
            new NetworkEventContext<ContextComponent>()
                .AddContextComponent(ContextComponent.NotificationStruct,
                    Notifications.GetNotification(NotificationType.OnValuableDestroyed))
                .AddContextComponent(ContextComponent.PlayerName, 
                    SemiFunc.PlayerGetName(interactionHandler.LastInteractionPlayer))
                .AddContextComponent(ContextComponent.ValuableName, valuable.name.Replace("(Clone)", ""))
                .AddContextComponent(ContextComponent.ValuableInitialValue, valuable.dollarValueOriginal);

        PluginEvents.SpawnToastEvent.RaiseEvent(context.Serialize(),
            REPOLib.Modules.NetworkingEvents.RaiseAll,
            SendOptions.SendReliable);
    }

}