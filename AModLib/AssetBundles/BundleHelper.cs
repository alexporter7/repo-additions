using System.IO;
using System.Reflection;
using UnityEngine;

namespace AModLib.AssetBundles;

public static class BundleHelper {

    public static GameObject GetPrefabFromBundle(AssetBundle assetBundle, string prefabName) {
        return assetBundle.LoadAsset<GameObject>(prefabName);
    }
    
    public static string GetAssetBundlePath(string bundleName, string basePath) {
        return Path.Combine(Path.GetDirectoryName(basePath)!, bundleName);
    }

}