using System;
using AModLib.api.delegates;
using AModLib.api.enums;
using AModLib.api.state;
using AModLib.weapon.gun;
using UnityEngine;


namespace AModLib.init;

public class ItemGunItemStates {

    public static readonly ItemState<ItemGunStates> Ready =
        new ItemState<ItemGunStates>(ItemGunStates.Ready)
            .AddValidNextState(ItemGunStates.Shooting)
            .AddValidNextState(ItemGunStates.Reloading)
            .SetStateSupplier(ReadyStateSupplier)
            .SetOnStateTick(ReadyStateTick);

    public static readonly ItemState<ItemGunStates> Shooting =
        new ItemState<ItemGunStates>(ItemGunStates.Shooting)
            .AddValidNextState(ItemGunStates.Cooldown)
            .AddValidNextState(ItemGunStates.NoAmmo)
            .SetStateSupplier(ShootingStateSupplier);

    public static readonly ItemState<ItemGunStates> Cooldown =
        new ItemState<ItemGunStates>(ItemGunStates.Cooldown)
            .AddValidNextState(ItemGunStates.Ready)
            .AddValidNextState(ItemGunStates.NoAmmo)
            .SetStateSupplier(CooldownStateSupplier)
            .SetOnStateEnter(CooldownOnEnter)
            .SetOnStateTick(CooldownStateTick);
    
    public static readonly ItemState<ItemGunStates> NoAmmo =
        new ItemState<ItemGunStates>(ItemGunStates.NoAmmo)
            .AddValidNextState(ItemGunStates.Reloading);
    
    public static readonly ItemState<ItemGunStates> Reloading =
        new ItemState<ItemGunStates>(ItemGunStates.Reloading)
            .AddValidNextState(ItemGunStates.Ready);
    
    /* Ready State */
    private static Action<Component> ReadyStateTick = delegate (Component component) {
        GunBase instance = component.GetComponent<GunBase>();
        if(instance.GetPhysGrabObject().heldByLocalPlayer && SemiFunc.InputDown(InputKey.Interact))
            instance.GetItemStateManager().RequestNextState();
    };
    private static Func<Component, ItemGunStates> ReadyStateSupplier = component => {
        GunBase instance = component.GetComponent<GunBase>();
        return instance.GetAmmoClip().HasAmmo() 
            ? ItemGunStates.Shooting 
            : ItemGunStates.NoAmmo;
    };

    /* Shooting State */
    private static Func<Component, ItemGunStates> ShootingStateSupplier = component => {
        GunBase instance = component.GetComponent<GunBase>();
        return instance.GetAmmoClip().HasAmmo()
            ? ItemGunStates.Cooldown
            : ItemGunStates.NoAmmo;
    };
    
    /* Cooldown State */
    private static Action<Component> CooldownOnEnter = delegate(Component component) {
        GunBase instance = component.GetComponent<GunBase>();
        instance.shootCooldownTimer = instance.shootCooldown;
    };
    private static Action<Component> CooldownStateTick = delegate(Component component) {
        GunBase instance = component.GetComponent<GunBase>();
        instance.shootCooldownTimer -= Time.deltaTime;
    };
    private static Func<Component, ItemGunStates> CooldownStateSupplier = component => ItemGunStates.Ready;

    /* No Ammo State */
    

    /* Reloading State */

}