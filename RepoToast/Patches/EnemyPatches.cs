using AModLib.Api.Network;
using ExitGames.Client.Photon;
using HarmonyLib;
using RepoToast.Notification;
using RepoToast.Plugin;

namespace RepoToast.Patches;

[HarmonyPatch(typeof(EnemyParent))]
public class EnemyPatches {

    [HarmonyPostfix]
    [HarmonyPatch("Spawn")]
    private static void OnEnemySpawnPostfix(EnemyParent __instance) {
        if (GameDirector.instance.currentState != GameDirector.gameState.Main
            || !__instance.Spawned)
            return;
        RepoToast.Logger.LogInfo($"Enemy status: {__instance.enemyName}: {__instance.Spawned}");
        EnemyHealth enemyHealth = __instance.Enemy.Health;
        NetworkEventContext<ContextComponent> ctx =
            new NetworkEventContext<ContextComponent>()
                .AddContextComponent(ContextComponent.MonsterName, __instance.enemyName)
                .AddContextComponent(ContextComponent.MonsterHealth, enemyHealth.health)
                .AddContextComponent(ContextComponent.NotificationStruct, 
                    Notifications.GetNotification(NotificationType.OnMonsterSpawn));
        PluginEvents.SpawnToastEvent.RaiseEvent(
            ctx.Serialize(),
            REPOLib.Modules.NetworkingEvents.RaiseAll,
            SendOptions.SendReliable);
    }

}

[HarmonyPatch(typeof(EnemyHealth))]
public class EnemyHealthPatches {

    [HarmonyPostfix]
    [HarmonyPatch("Death")]
    private static void OnEnemyDeathPostfix(EnemyHealth __instance) {
        if (GameDirector.instance.currentState != GameDirector.gameState.Main)
            return;
        Enemy       enemy       = __instance.enemy;
        EnemyParent enemyParent = enemy.EnemyParent;
        NetworkEventContext<ContextComponent> ctx =
            new NetworkEventContext<ContextComponent>()
                .AddContextComponent(ContextComponent.PlayerName, SemiFunc.PlayerGetName(SemiFunc.PlayerAvatarLocal()))
                .AddContextComponent(ContextComponent.MonsterName, enemyParent.enemyName)
                .AddContextComponent(ContextComponent.NotificationStruct, 
                    Notifications.GetNotification(NotificationType.OnMonsterKilled));
        PluginEvents.SpawnToastEvent.RaiseEvent(
            ctx.Serialize(),
            REPOLib.Modules.NetworkingEvents.RaiseAll,
            SendOptions.SendReliable);
    }

}