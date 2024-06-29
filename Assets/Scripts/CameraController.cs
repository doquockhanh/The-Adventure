using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public BoxCollider2D boundingBox; // Collider c?a h?p gi?i h?n

    private Vector3 minBounds;
    private Vector3 maxBounds;
    private Camera mainCamera;
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        mainCamera = Camera.main;
        minBounds = boundingBox.bounds.min;
        maxBounds = boundingBox.bounds.max;
        halfHeight = mainCamera.orthographicSize;
        halfWidth = halfHeight * mainCamera.aspect;
    }

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.z = transform.position.z;

        float clampedX = Mathf.Clamp(newPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(newPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, newPosition.z);
    }
}
