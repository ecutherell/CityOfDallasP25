using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RealTimeToggle : MonoBehaviour
{
    // Start is called before the first frame update
    public Toggle myToggle;
    //private bool camEnabled = false;
    public bool animationStarted = false;
    public GameObject holder;
    private Camera topDownCamera;
    private Pathway path;
    void Start()
    {
        GameObject PathwayHolder = GameObject.Find("Path Object");
        topDownCamera = GameObject.Find("TopDownCamera").GetComponent<Camera>();
        Pathway path = PathwayHolder.GetComponent<Pathway>();
        //topDownCamera = Gma
         myToggle = GetComponent<Toggle>();
        // myToggle.onValueChanged.AddListener(delegate {
        //     ToggleValueChanged(myToggle);
        // });
    }

    void ToggleValueChanged(Toggle change)
    {
        //CamChange cam = holder.GetComponent<CamChange>();
        GameObject PathwayHolder = GameObject.Find("Path Object");
        topDownCamera = GameObject.Find("TopDownCamera").GetComponent<Camera>();
        Pathway path = PathwayHolder.GetComponent<Pathway>();
        if(topDownCamera.enabled == true && myToggle.isOn == true)
            path.RTOn();
        else if(topDownCamera.enabled == false)
            path.RTOff();
             
        
    }

    // void Update()
    // {
    //     if(topDownCamera.enabled == true && myToggle.isOn == true)
    //         path.RTOn();
    //     else if(topDownCamera.enabled == false)
    //         path.RTOff();
    // }

}
