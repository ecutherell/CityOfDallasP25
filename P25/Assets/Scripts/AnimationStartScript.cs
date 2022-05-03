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
    private Pathway pathObj;
    void Start(){
        
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(ClickEvent);
    }

//test

    void ClickEvent()
    {

        Pathway pathobj = GameObject.Find("Path Object").GetComponent<Pathway>();
        PathwayScriptableObject t = Resources.Load<PathwayScriptableObject>("MainPathWay");
        pathobj.path = t;
        pathobj.setPath();
        GameObject btn = GameObject.Find("AnimStartBtn");
        GameObject secondbtn = GameObject.Find("SecondAnimStartBtn");
        secondbtn.SetActive(false);
        CamChange cam = camHolder.GetComponent<CamChange>();
        ToggleBtnCamChanger tog = GameObject.Find("Toggle").GetComponent<ToggleBtnCamChanger>();
        tog.animationStarted = true;
        CameraInitialize camI = GameObject.Find("EdgeFollow").GetComponent<CameraInitialize>();
        camI.moveCamera();

        if(cam.tg.interactable == true)
        {
            //do nothing
        }
        else if(cam.tg.interactable == false)
        {
            cam.switchToTrackerCamera();
        }
        
        btn.SetActive(false);
        Step_AutoanimateToggle.SetActive(true);

    }

}
