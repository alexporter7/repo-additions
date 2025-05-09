namespace MoreWeapons;

using REPOLib;
using UnityEngine;
using System.Collections;

public class PluginBundleLoader {

    public static void RegisterItems() {
        REPOLib.BundleLoader.LoadBundle("moreweapons.repobundle", assetBundle => {
            var item = assetBundle.LoadAsset<Item>("PM40Handgun");
            REPOLib.Modules.Items.RegisterItem(item);
        });
    }
    
    public static void LoadBundles() {
        BundleLoader.LoadBundle(
                "moreweapons.repobundle",
                OnBundleLoaded,
                loadContents: true
            );
    }

    static IEnumerator OnBundleLoaded(AssetBundle assetBundle) {
        MoreWeapons.Logger.LogInfo($"Loaded {assetBundle.name} successfully");
        yield break;
    }
    
}