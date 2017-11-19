﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    [SerializeField] private Shooter currentWeapon;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            enabled = false;
            return;
        }
        //When equip
        //GetComponentInChildren<Shooter>().isLocalPlayer = true;
    }

    void Update () {
		if (GameManager.Instance.InputController.Fire1 && currentWeapon)
        {
            if (currentWeapon.Fire()) {
                CmdFire();
            }
        }
	}

    [Command]
    void CmdFire()
    {
        var obj = Instantiate(currentWeapon.Prefab, currentWeapon.Muzzle.position, currentWeapon.Muzzle.rotation);
        NetworkServer.Spawn(obj);
    }
}
