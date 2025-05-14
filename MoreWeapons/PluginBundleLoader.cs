using System.Collections.Generic;
using System.IO;
using System.Reflection;
using REPOLib.Objects.Sdk;
using Unity.VisualScripting;

namespace MoreWeapons;

using REPOLib;
using UnityEngine;
using System.Collections;

public class PluginBundleLoader {
    public static Dictionary<string, Item> Items = new Dictionary<string, Item>();
    public static AssetBundle?             MoreWeaponsBundle;

    public static void LoadAssetBundle() {
        // REPOLib.BundleLoader.LoadBundle("moreweapons", assetBundle => {
        //     var item = assetBundle.LoadAsset<Item>("testitem");
        //     REPOLib.Modules.Items.RegisterItem(item);
        // });
        MoreWeaponsBundle = AssetBundle.LoadFromFile(GetAssetBundlePath("moreweapons"));
        // foreach (var allAssetName in MoreWeaponsBundle.GetAllAssetNames()) {
        //     MoreWeapons.Logger.LogInfo("Found asset: " + allAssetName + " | " + MoreWeaponsBundle.Contains(allAssetName));
        // }

        //Item test = GetItemFromBundle("assets/moreweapons/testitem.asset");
        //MoreWeapons.Logger.LogInfo("Item: " + test.itemName);
        Items.Add("pm40", GetItemFromBundle("PM40Handgun"));
        RegisterItems();
    }

    public static void RegisterItems() {
        foreach (var keyValuePair in Items) {
            //MoreWeapons.Logger.LogInfo("Item: " + keyValuePair.Value.itemName);
            REPOLib.Modules.Items.RegisterItem(keyValuePair.Value);
        }
    }

    public static void LoadBundles() {
        BundleLoader.LoadBundle(
            GetAssetBundlePath("moreweapons.repobundle"),
            OnBundleLoaded,
            loadContents: true
        );
    }

    static IEnumerator OnBundleLoaded(AssetBundle assetBundle) {
        MoreWeapons.Logger.LogInfo($"Loaded {assetBundle.name} successfully");
        yield break;
    }

    public static string GetAssetBundlePath(string bundleName) {
        return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
            bundleName);
    }

    public static GameObject GetGameObjectFromBundle(string assetName) {
        return MoreWeaponsBundle.LoadAsset<GameObject>(assetName);
    }

    public static Item GetItemFromBundle(string assetName) {
        return MoreWeaponsBundle.LoadAsset<Item>(assetName);
    }
}