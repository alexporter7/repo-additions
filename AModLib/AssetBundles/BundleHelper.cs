using UnityEngine;

namespace AModLib.AssetBundles;

public class BundleHelper {

    public static GameObject GetPrefabFromBundle(AssetBundle assetBundle, string prefabName) {
        return assetBundle.LoadAsset<GameObject>(prefabName);
    }

}