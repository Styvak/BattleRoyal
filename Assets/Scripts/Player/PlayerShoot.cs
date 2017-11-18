using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    private bool isAiming = false;

	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            isAiming = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            isAiming = false;
        }
	}
}
