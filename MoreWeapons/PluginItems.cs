using AModLib.AssetBundles;
using AModLib.Items;
using UnityEngine;

namespace MoreWeapons;

public class PluginItems {

    public static Item bulletHandgun = new ItemBuilder()
                                       .SetItemAssetName("PM40Handgun")
                                       .SetItemName("PM40 Handgun")
                                       .SetItemDescription("Shoots bullets, not sure what else to say")
                                       .SetItemType(SemiFunc.itemType.gun)
                                       .SetEmojiIcon(SemiFunc.emojiIcon.item_gun_shotgun)
                                       .SetPrefab(BundleHelper.GetPrefabFromBundle(PluginBundleLoader.MoreWeaponsBundle,
                                           "PM40Handgun"))
                                       .SetMaxAmountInShop(3)
                                       .BuildItem();

}