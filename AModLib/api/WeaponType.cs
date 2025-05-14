using System.Collections.Generic;

namespace AModLib.api;

public class WeaponType<T>(string type) {

    private HashSet<T> Weapons = new HashSet<T>();
    private string     Type    = type;

    public WeaponType<T> AddWeapon(T weapon) {
        Weapons.Add(weapon);
        return this;
    }

    public bool HasWeapon(T weapon) {
        return Weapons.Contains(weapon);
    }
}