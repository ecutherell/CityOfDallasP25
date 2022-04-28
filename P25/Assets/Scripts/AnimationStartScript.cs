using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimationStartScript : MonoBehaviour
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
       // Pathway pathobj = holder.GetComponent<Pathway>();
        //pathobj.Start();
        GameObject btn = GameObject.Find("AnimStartBtn");
        CamChange cam = camHolder.GetComponent<CamChange>();
        ToggleBtnCamChanger tog = GameObject.Find("Toggle").GetComponent<ToggleBtnCamChanger>();
        tog.animationStarted = true;
        cam.switchToTrackerCamera();
        btn.SetActive(false);
        Step_AutoanimateToggle.SetActive(true);
       // CameraInitialize initialCam =  GameObject.Find("EdgeFollow").GetComponent<CameraInitialize>();
        //pathobj.TurnAutoOn();
        //pathobj.startAnimation();

    }

}
