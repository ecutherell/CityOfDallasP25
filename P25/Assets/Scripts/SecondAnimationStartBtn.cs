using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class SecondAnimationStartBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public Button yourButton;
    public GameObject camHolder;
    public GameObject Step_AutoanimateToggle;
    void Start(){
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(ClickEvent);
    }

//test

    void ClickEvent()
    {
        // Destroy the gameObject after clicking on it
        Pathway pathobj = GameObject.Find("Path Object").GetComponent<Pathway>();
       //PathwayScriptableObject t = (PathwayScriptableObject)AssetDatabase.LoadAssetAtPath("Assets/Paths/BackupPathway.asset", typeof(PathwayScriptableObject));
        PathwayScriptableObject t = Resources.Load<PathwayScriptableObject>("BackupPathway");
        pathobj.path = t;
        ToggleBtnCamChanger tog = GameObject.Find("Toggle").GetComponent<ToggleBtnCamChanger>();
        tog.animationStarted = true;
        GameObject btn = GameObject.Find("SecondAnimStartBtn");
        CamChange cam = camHolder.GetComponent<CamChange>();
        cam.switchToTrackerCamera();
        btn.SetActive(false);
        Step_AutoanimateToggle.SetActive(true);
        //pathobj.TurnAutoOn();
        //pathobj.startAnimation();

    }
}
