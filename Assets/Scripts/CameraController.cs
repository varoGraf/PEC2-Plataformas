using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private float maxY, minY, maxX, minX;
    private Camera cam;
    [SerializeField]
    private Vector3 offset;
    private float lastX, lastY;

    void Start()
    {
        minY = 4;
        minX = 0;
        maxX = 205;
        cam = Camera.main;
        lastX = player.position.x;
        lastY = player.position.y;
    }

    void Update()
    {
        float posX;
        float posY;
        if (lastX > player.position.x)
        {
            posX = lastX + offset.x;
        }
        else
        {
            posX = player.position.x + offset.x;
            lastX = player.position.x;
        }
        if (player.position.x + offset.x >= maxX)
        {
            posX = maxX;
        }

        if (player.position.y + offset.y <= minY || player.position.y < minY)
        {
            posY = minY;
        }
        else
        {
            posY = player.position.y;
        }
        transform.position = new Vector3(posX, posY, -10);
    }
}
