using RepoToast.Notification;

namespace RepoToast;

public class Notifications {

    public static readonly NotificationStruct OnExtractUnlocked = new(
        NotificationType.OnExtractionUnlocked,
        "Extraction Unlocked",
        (playerName) => $"{playerName} has just unlocked an extraction point!");

}