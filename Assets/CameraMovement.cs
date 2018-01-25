using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 60f;
    public Vector3 offset;

    GameObject player;
    Vector3 velocity;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void LateUpdate ()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref velocity, speed * Time.fixedDeltaTime);	
	}
}
