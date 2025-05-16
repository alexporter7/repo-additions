using UnityEngine;

namespace MoreWeapons.Weapons.Gun;

public class AmmoClip : MonoBehaviour {
    
    public int Ammo = 0;

    public bool CanExtractAmmo(int amount = 1) {
        return Ammo >= amount;
    }

    public void ExtractAmmo(int amount = 1) {
        Ammo -= amount;
    }

    public bool HasAmmo() {
        return Ammo != 0;
    }
    
}