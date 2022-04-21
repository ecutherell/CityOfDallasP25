using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StepAutoButton : MonoBehaviour
{
    // Start is called before the first frame update
     public GameObject holder;
     Pathway pathobj;
     public Toggle parentToggle;
    void Start()
    {
          pathobj = holder.GetComponent<Pathway>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(parentToggle.isOn)
        {
            pathobj.TurnAutoOff();
        }
        else
        {
            pathobj.TurnAutoOn();
        }
    }
}
