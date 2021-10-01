using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float cameraDistance = 3.0f;

    void LateUpdate()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        transform.position = player.transform.position - playerController.currentVelocity.normalized * cameraDistance;
        transform.LookAt (player.transform.position);
        transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
    }
}