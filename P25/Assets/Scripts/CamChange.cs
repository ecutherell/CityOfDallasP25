using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChange : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;
    public Camera topDownCamera;
    public Camera trackerCamera;
    public Camera HighlandCam;


    void Start()
    {
     mainCamera = GameObject.Find("Broadway Camera").GetComponent<Camera>();
     topDownCamera = GameObject.Find("TopDownCamera").GetComponent<Camera>();
     trackerCamera = GameObject.Find("EdgeFollow").GetComponent<Camera>();
     HighlandCam = GameObject.Find("Highland Camera").GetComponent<Camera>();
        mainCamera.enabled = true;
        topDownCamera.enabled = false;
        trackerCamera.enabled = false;
        HighlandCam.enabled = false;

    }

    public void showTopDown()
    {
        Debug.Log("Showing top down");
        mainCamera.enabled = false;
        topDownCamera.enabled = true;
        //trackerCamera.enabled = false;
        HighlandCam.enabled = false;
    }

    public void switchToMain()
    {
        Debug.Log("Switching to main");
        mainCamera.enabled = true;
        topDownCamera.enabled = false;
        trackerCamera.enabled = false;
        HighlandCam.enabled = false;
    }
    public void switchToTrackerCamera()
    {
        Debug.Log("Switching to tracking");
        mainCamera.enabled = false;
        topDownCamera.enabled = false;
        trackerCamera.enabled = true;
        HighlandCam.enabled = false;
    }
    public void switchToHighlandCamera()
    {
        Debug.Log("Switching to highland camera");
        mainCamera.enabled = false;
        topDownCamera.enabled = false;
        trackerCamera.enabled = false;
        HighlandCam.enabled = true;
    }

}
