using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletChild : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem boom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(boom, other.transform.position, transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
