using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour {

    public float Damage { set {
            _damage = value;
        }
    }

    private float _damage = 1f;
    private List<Destructable> _destructables = new List<Destructable>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _destructables.Contains(other.GetComponent<Destructable>()))
        {
            _destructables.Remove(other.GetComponent<Destructable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _destructables.Add(other.GetComponent<Destructable>());
        }
    }

    public void Start()
    {
        StartCoroutine(InflictDamage());
    }

    IEnumerator InflictDamage()
    {
        while (true) {
            yield return new WaitForSeconds(0.5f);
            foreach (var destructable in _destructables)
                destructable.CmdTakeDamage(_damage);
        }
    }

}