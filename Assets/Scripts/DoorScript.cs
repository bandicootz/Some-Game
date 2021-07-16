using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Transform player;

    private float distanceToPlayer;
    [SerializeField]
    private float distanceToOpen;
    [SerializeField]
    private float openingSpeed;


    void Start()
    {
        player = GameObject.Find("BallPlayer").GetComponent<Transform>();
    }


    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= distanceToOpen)
        {
            transform.Translate(Vector3.down * openingSpeed * Time.deltaTime);
        }
    }
}
