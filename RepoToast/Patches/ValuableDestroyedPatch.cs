using ExitGames.Client.Photon;
using HarmonyLib;
using RepoToast.Notification;
using RepoToast.Plugin;

namespace RepoToast.Patches;

[HarmonyPatch(typeof (PhysGrabObject), "DestroyPhysGrabObject")]
internal class ValuableDestroyedPatch {

    [HarmonyPostfix]
    private static void OnValuableDestroyed(PhysGrabObject __instance) {
        ValuableObject valuable = __instance.GetComponent<ValuableObject>();
        NotificationContext context = 
            new NotificationContext(Notifications.GetNotification(NotificationType.OnValuableDestroyed))
                .AddContextComponent(ContextComponent.PlayerName, SemiFunc.PlayerGetName(SemiFunc.PlayerAvatarLocal()))
                .AddContextComponent(ContextComponent.ValuableName, valuable.name)
                .AddContextComponent(ContextComponent.ValuableInitialValue, valuable.dollarValueOriginal);
        PluginEvents.SpawnToastEvent.RaiseEvent(context.Serialize(), 
            REPOLib.Modules.NetworkingEvents.RaiseAll, 
            SendOptions.SendReliable);
    }

}