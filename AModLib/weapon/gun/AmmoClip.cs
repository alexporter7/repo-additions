using System.Collections.Generic;
using AModLib.api;
using UnityEngine;
using UnityEngine.Serialization;

namespace AModLib.weapon.gun;

public class AmmoClip : MonoBehaviour {
    
    public int Ammo;

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