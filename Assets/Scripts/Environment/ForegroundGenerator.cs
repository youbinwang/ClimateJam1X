using UnityEngine;
using System.Collections.Generic;

public class ForegroundScroller : MonoBehaviour
{
    public GameObject foregroundGameojbect;

    private float foregroundWidth;
    private float foregroundY;
    private float foregroundZ;

    private Camera mainCamera;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private GameObject leftForeground;
    private GameObject centerForeground;
    private GameObject rightForeground;

    void Start()
    {
        mainCamera = Camera.main;
        cameraTransform = mainCamera.transform;
        lastCameraPosition = cameraTransform.position;
        
        // Get Foreground Width
        SpriteRenderer spriteRenderer = foregroundGameojbect.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Bounds bounds = spriteRenderer.bounds;
            foregroundWidth = bounds.size.x;
        }

        foregroundY = foregroundGameojbect.transform.position.y;
        foregroundZ = foregroundGameojbect.transform.position.z;
        
        centerForeground = Instantiate(foregroundGameojbect, new Vector3(cameraTransform.position.x, foregroundY, foregroundZ), Quaternion.identity);
        leftForeground = Instantiate(foregroundGameojbect, new Vector3(cameraTransform.position.x - foregroundWidth, foregroundY, foregroundZ), Quaternion.identity);
        rightForeground = Instantiate(foregroundGameojbect, new Vector3(cameraTransform.position.x + foregroundWidth, foregroundY, foregroundZ), Quaternion.identity);
    }

    void Update()
    {
        float cameraDeltaX = cameraTransform.position.x - lastCameraPosition.x;

        if (Mathf.Abs(cameraDeltaX) >= foregroundWidth)
        {
            if (cameraDeltaX > 0)
            {
                Destroy(leftForeground);
                leftForeground = centerForeground;
                centerForeground = rightForeground;
                rightForeground = Instantiate(foregroundGameojbect, new Vector3(centerForeground.transform.position.x + foregroundWidth, foregroundY, foregroundZ), Quaternion.identity);
            }
            else if (cameraDeltaX < 0)
            {
                Destroy(rightForeground);
                rightForeground = centerForeground;
                centerForeground = leftForeground;
                leftForeground = Instantiate(foregroundGameojbect, new Vector3(centerForeground.transform.position.x - foregroundWidth, foregroundY, foregroundZ), Quaternion.identity);
            }

            lastCameraPosition = cameraTransform.position;
        }
    }
}