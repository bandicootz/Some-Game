using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public GameObject explosionChild;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject child = Instantiate(explosionChild, transform.position, transform.rotation);
            SphereCollider childCol = child.gameObject.GetComponent<SphereCollider>();
            childCol.radius *= transform.localScale.x;
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Door"))
        {
            Destroy(gameObject);
        }
    }
}
