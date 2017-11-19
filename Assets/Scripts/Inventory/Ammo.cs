﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : InventoryItem {

    [SerializeField]private WeaponType _weaponType;
    public WeaponType WeaponType{
        get { return _weaponType; }
    }

    [SerializeField]private int _count;
    public int Count{
        get { return _count; }
    }

    public void AddAmmo(int nb)
    {
        _count += nb;
    }

    public void SubAmmo(int nb)
    {
        _count -= nb;
    }
	
}
