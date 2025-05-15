using System;
using AModLib.api.enums;
using AModLib.api.state;
using AModLib.init;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

namespace AModLib.weapon.gun;

public class GunBase : MonoBehaviour {

    private ItemStateManager<ItemGunStates> ItemStateManager;

    private PhysGrabObject               physGrabObject;
    private AmmoClip                     ammoClip { get; set; }
    private PhotonView                   photonView;
    private PhysGrabObjectImpactDetector impactDetector;

    [Range(0f, 100f)] public float range             = 90f;
    [Range(0f, 3f)]   public float recoilForce       = 1f;
    [Range(0f, 3f)]   public float cameraShakeFactor = 1f;
    [Range(0f, 30f)]  public float shootCooldown     = 1f;
    [Range(0f, 20f)]  public float holdDistance      = 1f;

    public float shootCooldownTimer = 1f;

    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    public Transform  muzzleTransform;
    public Transform  triggerTransform;
    public Sound      shot;
    public Sound      shotGlobal;
    public Sound      shotNoAmmo;
    public Sound      shotHit;

    private void Start() {
        ItemStateManager =
            new ItemStateManager<ItemGunStates>()
                .RegisterState(ItemGunStates.Ready, ItemGunItemStates.Ready)
                .RegisterState(ItemGunStates.Shooting, ItemGunItemStates.Shooting)
                .RegisterState(ItemGunStates.NoAmmo, ItemGunItemStates.NoAmmo)
                .RegisterState(ItemGunStates.Cooldown, ItemGunItemStates.Cooldown)
                .RegisterState(ItemGunStates.Reloading, ItemGunItemStates.Reloading)
                .RegisterComponentInstance(this);
        
        physGrabObject = GetComponent<PhysGrabObject>();
        ammoClip       = GetComponent<AmmoClip>();
        photonView     = GetComponent<PhotonView>();
        impactDetector = GetComponent<PhysGrabObjectImpactDetector>();

        shootCooldownTimer = shootCooldown;
    }

    private void Update() {
        if (physGrabObject.grabbed && physGrabObject.grabbedLocal)
            PhysGrabber.instance.OverrideGrabDistance(holdDistance);

        //TODO: Trigger Animation

        if (SemiFunc.IsMasterClientOrSingleplayer())
            UpdateServer();
    }

    private void UpdateServer() {
        if (physGrabObject.grabbed) {
        }
    }

    public ItemStateManager<ItemGunStates> GetItemStateManager() {
        return ItemStateManager;
    }

    public PhysGrabObject GetPhysGrabObject() {
        return physGrabObject;
    }

    public AmmoClip GetAmmoClip() {
        return ammoClip;
    }

}