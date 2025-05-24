using ExitGames.Client.Photon;
using OdinSerializer;

namespace AModLib.Api.Network;

public static class NetworkDeserializer {

    public static T DeserializeEventData<T>(EventData eventData) {
        return SerializationUtility.DeserializeValue<T>((byte[])eventData.CustomData, DataFormat.Binary);
    }

}