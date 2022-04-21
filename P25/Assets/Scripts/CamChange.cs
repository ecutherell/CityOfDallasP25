using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChange : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;
    public Camera topDownCamera;


    void Start()
    {
     mainCamera = GameObject.Find("Broadway Camera").GetComponent<Camera>();
     topDownCamera = GameObject.Find("TopDownCamera").GetComponent<Camera>();
    }

    public void showTopDown()
    {
        Debug.Log("Showing top down");
        mainCamera.enabled = false;
        topDownCamera.enabled = true;
    }

    public void switchToMain()
    {
        Debug.Log("Switching to main");
        mainCamera.enabled = true;
        topDownCamera.enabled = false;
    }

}
