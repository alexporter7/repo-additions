using TMPro;
using UnityEngine;

namespace RepoToast;

public class ToastNotification : MonoBehaviour {

    private TextMeshProUGUI Header;
    private TextMeshProUGUI Description;
    public  GameObject      ToastPrefab;
    public  string          HeaderText;
    public  string          DescriptionText;


    private void Awake() {
        ToastPrefab = gameObject;
        foreach (TextMeshProUGUI textComponent in ToastPrefab.GetComponentsInChildren<TextMeshProUGUI>()) {
            if (textComponent.name == "Header") Header = textComponent;
            else Description                           = textComponent;
        }
    }

    private void Start() {
        Header.text      = HeaderText;
        Description.text = DescriptionText;
    }

}