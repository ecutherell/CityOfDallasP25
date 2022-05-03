using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleBtnCamChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public Toggle myToggle;
    private bool camEnabled = false;
    public bool animationStarted = false;
    private GameObject camHolder;
    private CamChange cams;
    public GameObject holder;
    void Start()
    {
        camHolder = GameObject.Find("CameraChangeObject");
        cams = camHolder.GetComponent<CamChange>();
        myToggle = GetComponent<Toggle>();
        myToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(myToggle);
        });
    }

    void ToggleValueChanged(Toggle change)
    {
        CamChange cam = holder.GetComponent<CamChange>();

        if(animationStarted == false)
        {
            if(!camEnabled)
            {
                cam.showTopDown();
                camEnabled = true;
            }

            else 
            {
                cam.switchToMain();
                camEnabled = false;
            }
        }

        else
        {
            if(!camEnabled)
            {
                cam.showTopDown();
                camEnabled = true;
            }

            else 
            {
                cam.switchToTrackerCamera();
                camEnabled = false;
            }     
        }
             
        
    }

}
