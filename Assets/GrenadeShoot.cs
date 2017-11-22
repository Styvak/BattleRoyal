using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class GrenadeShoot : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float timeToLive;

    private void Start()
    {
        Destroy(gameObject, timeToLive);
        GetComponent<Rigidbody>().AddForce(new Vector3(transform.forward.x,transform.forward.y + 1.3f, transform.forward.z)  * speed);
    }

    private void OnDestroy()
    {
        //boom
    }
}
