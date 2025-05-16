using UnityEngine;


namespace AModLib.Items;

public class ItemBuilder {

    private bool   Disabled          = false;
    private string ItemAssetName     = "item";
    private string ItemName          = "item";
    private string ItemDescription   = "Description";
    private int    MaxAmount         = 1;
    private int    MaxAmountInShop   = 1;
    private bool   MaxPurchase       = false;
    private int    MaxPurchaseAmount = 1;
    private bool   PhysicalItem      = true;

    private SemiFunc.itemType           ItemType           = SemiFunc.itemType.drone;
    private SemiFunc.emojiIcon          EmojiIcon          = SemiFunc.emojiIcon.drone_heal;
    private SemiFunc.itemVolume         ItemVolume         = SemiFunc.itemVolume.small;
    private SemiFunc.itemSecretShopType ItemSecretShopType = SemiFunc.itemSecretShopType.none;
    private ColorPresets                ColorPreset;
    private GameObject                  Prefab;
    private Value                       Value;
    private Quaternion                  SpawnRotationOffset;

    public ItemBuilder SetDisabled(bool disabled) {
        Disabled = disabled;
        return this;
    }

    public ItemBuilder SetItemAssetName(string itemAssetName) {
        ItemAssetName = itemAssetName;
        return this;
    }

    public ItemBuilder SetItemName(string itemName) {
        ItemName = itemName;
        return this;
    }

    public ItemBuilder SetItemDescription(string itemDescription) {
        ItemDescription = itemDescription;
        return this;
    }

    public ItemBuilder SetItemType(SemiFunc.itemType itemType) {
        ItemType = itemType;
        return this;
    }

    public ItemBuilder SetEmojiIcon(SemiFunc.emojiIcon emojiIcon) {
        EmojiIcon = emojiIcon;
        return this;
    }

    public ItemBuilder SetItemVolume(SemiFunc.itemVolume itemVolume) {
        ItemVolume = itemVolume;
        return this;
    }

    public ItemBuilder SetItemShopType(SemiFunc.itemSecretShopType itemSecretShopType) {
        ItemSecretShopType = itemSecretShopType;
        return this;
    }

    public ItemBuilder SetColorPreset(ColorPresets colorPresets) {
        ColorPreset = colorPresets;
        return this;
    }

    public ItemBuilder SetPrefab(GameObject prefab) {
        Prefab = prefab;
        return this;
    }

    public ItemBuilder SetValue(Value value) {
        Value = value;
        return this;
    }

    public ItemBuilder SetMaxAmount(int maxAmount) {
        MaxAmount = maxAmount;
        return this;
    }

    public ItemBuilder SetMaxAmountInShop(int maxAmountInShop) {
        MaxAmountInShop = maxAmountInShop;
        return this;
    }

    public ItemBuilder SetMaxPurchase(bool maxPurchase) {
        MaxPurchase = maxPurchase;
        return this;
    }

    public ItemBuilder SetMaxPurchaseAmount(int maxPurchaseAmount) {
        MaxPurchaseAmount = maxPurchaseAmount;
        return this;
    }

    public ItemBuilder SetSpawnRotationOffset(Quaternion spawnRotationOffset) {
        SpawnRotationOffset = spawnRotationOffset;
        return this;
    }

    public ItemBuilder SetPhysicalItem(bool physicalItem) {
        PhysicalItem = physicalItem;
        return this;
    }

    public Item BuildItem() {
        Item buildItem = ScriptableObject.CreateInstance<Item>();
        buildItem.disabled            = Disabled;
        buildItem.itemAssetName       = ItemAssetName;
        buildItem.itemName            = ItemName;
        buildItem.description         = ItemDescription;
        buildItem.maxAmount           = MaxAmount;
        buildItem.maxAmountInShop     = MaxAmountInShop;
        buildItem.maxPurchase         = MaxPurchase;
        buildItem.maxPurchaseAmount   = MaxPurchaseAmount;
        buildItem.physicalItem        = PhysicalItem;
        buildItem.itemType            = ItemType;
        buildItem.emojiIcon           = EmojiIcon;
        buildItem.itemVolume          = ItemVolume;
        buildItem.itemSecretShopType  = ItemSecretShopType;
        buildItem.colorPreset         = ColorPreset;
        buildItem.prefab              = Prefab;
        buildItem.value               = Value;
        buildItem.spawnRotationOffset = SpawnRotationOffset;
        return buildItem;
    }

}