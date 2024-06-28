using UnityEngine;
using System.Collections.Generic;

public class GroundAssetsScroller : MonoBehaviour
{
    public GameObject groundAssetsGameojbect;

    private float groundAssetsWidth;
    private float groundAssetsY;
    private float groundAssetsZ;

    private Camera mainCamera;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private GameObject leftGroundAssets;
    private GameObject centerGroundAssets;
    private GameObject rightGroundAssets;

    void Start()
    {
        mainCamera = Camera.main;
        cameraTransform = mainCamera.transform;
        lastCameraPosition = cameraTransform.position;
        
        // Get Foreground Width
        SpriteRenderer spriteRenderer = groundAssetsGameojbect.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Bounds bounds = spriteRenderer.bounds;
            groundAssetsWidth = bounds.size.x;
        }

        groundAssetsY = groundAssetsGameojbect.transform.position.y;
        groundAssetsZ = groundAssetsGameojbect.transform.position.z;
        
        centerGroundAssets = Instantiate(groundAssetsGameojbect, new Vector3(cameraTransform.position.x, groundAssetsY, groundAssetsZ), Quaternion.identity);
        leftGroundAssets = Instantiate(groundAssetsGameojbect, new Vector3(cameraTransform.position.x - groundAssetsWidth, groundAssetsY, groundAssetsZ), Quaternion.identity);
        rightGroundAssets = Instantiate(groundAssetsGameojbect, new Vector3(cameraTransform.position.x + groundAssetsWidth, groundAssetsY, groundAssetsZ), Quaternion.identity);
    }

    void Update()
    {
        float cameraDeltaX = cameraTransform.position.x - lastCameraPosition.x;

        if (Mathf.Abs(cameraDeltaX) >= groundAssetsWidth)
        {
            if (cameraDeltaX > 0)
            {
                Destroy(leftGroundAssets);
                leftGroundAssets = centerGroundAssets;
                centerGroundAssets = rightGroundAssets;
                rightGroundAssets = Instantiate(groundAssetsGameojbect, new Vector3(centerGroundAssets.transform.position.x + groundAssetsWidth, groundAssetsY, groundAssetsZ), Quaternion.identity);
            }
            else if (cameraDeltaX < 0)
            {
                Destroy(rightGroundAssets);
                rightGroundAssets = centerGroundAssets;
                centerGroundAssets = leftGroundAssets;
                leftGroundAssets = Instantiate(groundAssetsGameojbect, new Vector3(centerGroundAssets.transform.position.x - groundAssetsWidth, groundAssetsY, groundAssetsZ), Quaternion.identity);
            }

            lastCameraPosition = cameraTransform.position;
        }
    }
}