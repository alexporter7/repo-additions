using AModLib.api.enums;
using AModLib.api.state;


namespace AModLib.init;

public class ItemGunItemStates {

    public static readonly ItemState<ItemGunStates> Ready =
        new ItemState<ItemGunStates>(ItemGunStates.Ready)
            .AddValidNextState(ItemGunStates.Shooting)
            .AddValidNextState(ItemGunStates.Reloading);

    public static readonly ItemState<ItemGunStates> Shooting =
        new ItemState<ItemGunStates>(ItemGunStates.Shooting)
            .AddValidNextState(ItemGunStates.Cooldown);

    public static readonly ItemState<ItemGunStates> Cooldown =
        new ItemState<ItemGunStates>(ItemGunStates.Cooldown)
            .AddValidNextState(ItemGunStates.Ready)
            .AddValidNextState(ItemGunStates.NoAmmo);
    
    public static readonly ItemState<ItemGunStates> NoAmmo =
        new ItemState<ItemGunStates>(ItemGunStates.NoAmmo)
            .AddValidNextState(ItemGunStates.Reloading);
    
    public static readonly ItemState<ItemGunStates> Reloading =
        new ItemState<ItemGunStates>(ItemGunStates.Reloading)
            .AddValidNextState(ItemGunStates.Ready);

}