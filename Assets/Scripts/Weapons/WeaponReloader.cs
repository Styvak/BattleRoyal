using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloader : MonoBehaviour {

    [SerializeField] private int maxAmmo;
    [SerializeField] private float reloadTime;
    [SerializeField] private int clipSize;

    private int ammo;
    private int shotsFiredInClip;
    private bool isReloading;

    public int RoundsRemainingClip {
        get {
            return clipSize - shotsFiredInClip;
        }
    }

    public bool IsReloading {
        get {
            return isReloading;
        }
    }

    public void Reload()
    {
        if (isReloading)
            return;
        isReloading = true;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(reloadTime);
        ExecuteReload();
    }

    void ExecuteReload()
    {
        isReloading = false;
        ammo -= shotsFiredInClip;
        shotsFiredInClip = 0;

        if (ammo < 0)
        {
            ammo = 0;
            shotsFiredInClip -= ammo;
        }
    }

    public void TakeFromClip(int amount)
    {
        shotsFiredInClip += amount;
    }
}
