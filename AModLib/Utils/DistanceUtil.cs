using System.Collections.Generic;
using UnityEngine;

namespace AModLib.Utils;

public static class DistanceUtil {

    public static PlayerAvatar GetClosestPlayerToPos(List<PlayerAvatar> players, Vector3 targetPosition) {
        if (players.Count == 1)
            return players[0];
        PlayerAvatar closestPlayer = players[0];
        foreach(PlayerAvatar player in players)
            if (Vector3.Distance(player.clientPosition, targetPosition) <
                Vector3.Distance(closestPlayer.clientPosition, targetPosition))
                closestPlayer = player;
        return closestPlayer;
    }

}