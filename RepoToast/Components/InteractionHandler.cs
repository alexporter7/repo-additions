using System;
using System.Collections.Generic;
using UnityEngine;

namespace RepoToast.Components;

public class InteractionHandler : MonoBehaviour {

    public PlayerAvatar       FirstGrabPlayer;
    public List<PlayerAvatar> PlayersInteracted = [];
    public PlayerAvatar       LastInteractionPlayer;

    private void Awake() {
        RepoToast.Logger.LogDebug($"An InteractionHandler has been attachted to [{gameObject.name}]");
    }

    public void AddInteraction(PlayerAvatar player) {

        if (FirstGrabPlayer == null) {
            RepoToast.Logger.LogDebug($"Setting First interaction on an object from " +
                                      $"player [{SemiFunc.PlayerGetName(player)}]");
            FirstGrabPlayer = player;   
        }

        if (!PlayersInteracted.Contains(player)) {
            RepoToast.Logger.LogDebug($"Adding interaction on an object from " +
                                      $"player [{SemiFunc.PlayerGetName(player)}]");
            PlayersInteracted.Add(player);
        }
        
        LastInteractionPlayer = player;
    }

    public PlayerAvatar GetLastInteraction() {
        return LastInteractionPlayer;
    }

    public bool HasPlayerInteractedWithObject(PlayerAvatar player) {
        return PlayersInteracted.Contains(player);
    }

}