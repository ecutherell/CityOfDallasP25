using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RealTimeToggle : MonoBehaviour
{
    // Start is called before the first frame update
    public Toggle myToggle;
    public bool animationStarted = false;
    public GameObject holder;
    private Camera topDownCamera;
    private Pathway path;
    void Start()
    {
        GameObject PathwayHolder = GameObject.Find("Path Object");
        topDownCamera = GameObject.Find("TopDownCamera").GetComponent<Camera>();
        Pathway path = PathwayHolder.GetComponent<Pathway>();
         myToggle = GetComponent<Toggle>();
        myToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(myToggle);
        });
    }

    void ToggleValueChanged(Toggle change)
    {
        GameObject PathwayHolder = GameObject.Find("Path Object");
        topDownCamera = GameObject.Find("TopDownCamera").GetComponent<Camera>();
        Pathway path = PathwayHolder.GetComponent<Pathway>();
        Debug.Log(topDownCamera.enabled + " " + myToggle.isOn);
        if(topDownCamera.enabled == true && myToggle.isOn == true)
        {
            
            path.realtimeAnimate = true;
        }
            
        else if(topDownCamera.enabled == false)
            path.realtimeAnimate = false;
             
        
    }



}
