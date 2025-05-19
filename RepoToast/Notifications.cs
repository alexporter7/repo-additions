using RepoToast.Notification;

namespace RepoToast;

public class Notifications {

    public static NotificationStruct OnExtractUnlocked = new NotificationStruct(
        NotificationType.OnExtractionUnlocked,
        "Extraction Unlocked",
        (playerName) => $"{playerName} has just unlocked an extraction point!");

}