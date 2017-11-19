using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [SerializeField] private float rateOfFire;
    [SerializeField] protected GameObject projectile;

    [SerializeField] protected Transform muzzle;

    public bool isLocalPlayer = false;

    public GameObject Prefab {
        get {
            return projectile;
        }
    }

    public Transform Muzzle {
        get
        {
            return muzzle;
        }
    }

    private float nextFireAllowed;
    protected bool canFire;
    protected Transform spine;

    private void Awake()
    {
        spine = transform.parent;
    }

    public virtual bool Fire()
    {
        canFire = false;

        if (Time.time < nextFireAllowed)
            return false;
        nextFireAllowed = Time.time + rateOfFire;
        canFire = true;
        return true;
    }

    void LateUpdate()
    {
        if (isLocalPlayer)
            PositionSpine();
    }

    void PositionSpine()
    {
        Transform mainCamT = Camera.main.transform;
        Vector3 mainCamPos = mainCamT.position;
        Vector3 dir = mainCamT.forward;
        Ray ray = new Ray(mainCamPos, dir);

        spine.LookAt(ray.GetPoint(500));

        //Vector3 eulerAngleOffset = new Vector3(3.67f, 44.95f, 7.45f);
        //spine.Rotate(eulerAngleOffset);
    }
}
