using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    [SerializeField] private Shooter currentWeapon;

	void Update () {
		if (GameManager.Instance.InputController.Fire1 && currentWeapon)
        {
            currentWeapon.Fire();
        }
	}
}
