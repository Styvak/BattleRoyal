using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour {

    public float timeValue = 0.01f;

    private List<Collider> colliders = new List<Collider>();

    private void Start()
    {
        StartCoroutine(DestroyShield(10.0f));
    }

    private void OnTriggerEnter(Collider other)
    {
        colliders.Add(other);
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            if (other.tag == "Player")
                other.gameObject.GetComponent<TimeBehaviour>().TimeController(timeValue);
            else if (other.gameObject.GetComponent<TimeBehaviour>())
                other.gameObject.GetComponent<TimeBehaviour>().TimeController(timeValue / 10f);
        }
    }

    IEnumerator DestroyShield(float duration)
    {
        yield return new WaitForSeconds(duration);
        foreach (var col in colliders)
        {
            if (col.gameObject.GetComponent<Rigidbody>() && col.gameObject.GetComponent<TimeBehaviour>())
            {
                col.gameObject.GetComponent<TimeBehaviour>().TimeController(1f);
            }
        }
        Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            other.gameObject.GetComponent<TimeBehaviour>().TimeController(1f);
        }
    }
}
