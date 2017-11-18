using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float timeToLive;
    [SerializeField] private float damage;

    private void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    void Update () {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
	}

    protected virtual void OnTriggerEnter(Collider other)
    {
        var destructable = other.transform.GetComponent<Destructable>();

        if (!destructable)
            return;
        destructable.TakeDamage(damage);
    }
}
