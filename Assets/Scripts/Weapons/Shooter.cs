using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    [SerializeField] private float rateOfFire;
    [SerializeField] protected Projectile projectile;

    [SerializeField] protected Transform muzzle;

    private float nextFireAllowed;
    protected bool canFire;
	
	public virtual void Fire()
    {
        canFire = false;

        if (Time.time < nextFireAllowed)
            return;
        nextFireAllowed = Time.time + rateOfFire;
        canFire = true;
    }
}
