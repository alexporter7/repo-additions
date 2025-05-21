using AModLib.Api.Enums;

namespace AModLib.Api.Network;

public static class ClientHelper {

    public static ClientType GetClientType() {
        return AModLib.Instance.ClientType;
    }

    public static bool IsMultiplayerGame() {
        return GameManager.Multiplayer();
    }
    
    public static bool IsSingleplayerGame() {
        return !GameManager.Multiplayer();
    }

    public static bool IsMasterClient() {
        return AModLib.Instance.ClientType == ClientType.Host;
    }
    
    public static bool IsRemoteClient() {
        return AModLib.Instance.ClientType == ClientType.Client;
    }

    public static bool IsLocalClient() {
        return AModLib.Instance.ClientType == ClientType.Local;
    }

}