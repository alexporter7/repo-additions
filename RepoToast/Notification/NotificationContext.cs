using System.Collections.Generic;
using System.Linq;
using OdinSerializer;

namespace RepoToast.Notification;

public class NotificationContext(NotificationStruct notificationStruct) {

    public readonly Dictionary<ContextComponent, object> ContextComponents  = [];
    public readonly NotificationStruct                   NotificationStruct = notificationStruct;

    public NotificationContext AddContextComponent(ContextComponent key, object value) {
        ContextComponents.Add(key, value);
        return this;
    }

    public object GetProp(ContextComponent prop) {
        return ContextComponents[prop];
    }

    public byte[] Serialize() => SerializationUtility.SerializeValue(this, DataFormat.Binary);

}