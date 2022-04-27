using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackupStepAutoBtn : MonoBehaviour
{
    // Start is called before the first frame update
     public GameObject holder;
     Pathway pathobj;
     public Toggle parentToggle;
    void Start()
    {
          pathobj = holder.GetComponent<Pathway>();
        parentToggle = GetComponent<Toggle>();
        parentToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(parentToggle);
        });
        if(!parentToggle.isOn)
        {
            //pathobj.EndAnimation();
            //pathobj.TurnAutoOff();
            //pathobj.
            //pathobj.setBackupPath();
            pathobj.startAnimation();
        }

    }

    // Update is called once per frame
    void ToggleValueChanged(Toggle parentToggle)
    {
        
        if(parentToggle.isOn)
        {
            pathobj.TurnAutoOn();
            //pathobj.EndAnimation();
            //pathobj.startAnimation();
        }

    }
}
