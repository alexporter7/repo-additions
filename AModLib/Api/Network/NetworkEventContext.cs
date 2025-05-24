using System.Collections.Generic;
using OdinSerializer;

namespace AModLib.Api.Network;

public class NetworkEventContext<T> {

    public readonly Dictionary<T, object> ContextComponents = [];

    public NetworkEventContext<T> AddContextComponent(T key, object value) {
        ContextComponents.Add(key, value);
        return this;
    }

    public object GetProp(T contextComponent) {
        return ContextComponents[contextComponent];
    }

    public K GetProp<K>(T contextComponent) {
        return (K)ContextComponents[contextComponent];
    }

    public byte[] Serialize() => SerializationUtility.SerializeValue(this, DataFormat.Binary);

}