/*===============================================================================
Copyright (c) 2016-2018 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using Vuforia;

public class CloudRecoScanLine : MonoBehaviour
{
    public Camera ARCamera;
    public CloudRecoBehaviour CloudRecognitionBehaviour;
    
    float mTime;
    bool mMovingDown = true;
    bool mIsCacheEnabled;
    Renderer mRenderer;
        
    const float SCAN_DURATION_IN_SECONDS = 4;
    
    bool CloudEnabled
    {
        get { return CloudRecognitionBehaviour && CloudRecognitionBehaviour.RecoStarted; }
    }

    void Start()
    {
        mRenderer = GetComponent<Renderer>();
        // Cache the Cloud enable state so that we can reset the scanline
        // when the enabled state changes
        mIsCacheEnabled = CloudEnabled;
    }

    void Update()
    {
        if (mIsCacheEnabled != CloudEnabled)
        {
            mIsCacheEnabled = CloudEnabled;
            // Reset the ScanLine position when Cloud enabled state changes
            mTime = 0;
            mMovingDown = true;
        }

        mRenderer.enabled = CloudEnabled; // show/hide scanline

        if (CloudEnabled)
        {
            var currentLocationInTime = mTime / SCAN_DURATION_IN_SECONDS;
            mTime += Time.deltaTime;
            if (currentLocationInTime > 1)
            {
                // invert direction
                mMovingDown = !mMovingDown;
                currentLocationInTime = 0;
                mTime = 0;
            }

            // Get the main camera
            var viewAspect = ARCamera.pixelWidth / (float)ARCamera.pixelHeight;
            var fovY = Mathf.Deg2Rad * ARCamera.fieldOfView;
            var depth = 1.02f * ARCamera.nearClipPlane;
            var viewHeight = 2 * depth * Mathf.Tan(0.5f * fovY);
            var viewWidth = viewHeight * viewAspect;

            // Position the mesh
            var y = -0.5f * viewHeight + currentLocationInTime * viewHeight;
            if (mMovingDown)
                y *= -1;

            transform.localPosition = new Vector3(0, y, depth);
            // Scale the quad mesh to fill the camera view
            var scaleX = 1.02f * viewWidth;
            var scaleY = scaleX / 32;
            transform.localScale = new Vector3(scaleX, scaleY, 1.0f);
        }
    }
}
