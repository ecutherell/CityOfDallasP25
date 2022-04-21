using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleBtnCamChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public Toggle myToggle;
    private bool camEnabled = false;
    public GameObject holder;
    void Start()
    {
        myToggle = GetComponent<Toggle>();
        myToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(myToggle);
        });
    }

    void ToggleValueChanged(Toggle change)
    {
        CamChange cam = holder.GetComponent<CamChange>();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
