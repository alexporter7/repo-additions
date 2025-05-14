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
    private AmmoClip                     ammoClip;
    private PhotonView                   photonView;
    private PhysGrabObjectImpactDetector impactDetector;

    public float range;
    public float recoilForce;
    public float cameraShakeFactor;
    public float shootCooldown;
    public float holdDistance;

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
                .RegisterState(ItemGunStates.Reloading, ItemGunItemStates.Reloading);
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

}