using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private Vector3 offset;


    void Start()
    {
        player = GameObject.Find("BallPlayer");
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
