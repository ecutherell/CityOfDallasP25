using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollows : MonoBehaviour
{
    public GameObject target;
    private Vector3 cameraPosition;
    private Vector3 offset;
    private GameObject parentCam;
    private GameObject TopCamera;
    private Pathway startTower;
    public bool startedTracking = false;
    public Pathway p;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position - target.transform.position + (0,1,2);
        //Quaternion rotation = Quaternion.Euler(0, 30, 0);
        //this.enabled = false;

    }

    public void StartTracking()
    {
        startTower = GameObject.Find("Path Object").GetComponent<Pathway>();
        offset = new Vector3(350,150,0);
        Camera cam = this.GetComponent<Camera>(); cam.fieldOfView = 60;
        TopCamera = GameObject.Find("TopDownCamera");
        //parentCam = GetComponent<Camera>();
        this.transform.Rotate(30f,-90f,0f,0f);
        startedTracking = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //only use the tracking camera if in slowed down animation
        if(p.realtimeAnimate == false && startedTracking == true)
        {
            target = GameObject.Find("Sphere");
            cameraPosition = target.transform.position + offset;
            transform.position = cameraPosition;
        }
    }

    private IEnumerator GoToTop()
    {

                float startTime = Time.time;
                Vector3 startPos = cameraPosition;
                Vector3 endPos = TopCamera.transform.position;
                Vector3 pos = startPos;
                float  animationDuration = 5f;

                while(pos != endPos)
                {
                    float t = (Time.time - startTime) / animationDuration;
                    pos = Vector3.Lerp(startPos, endPos, t);
                    this.transform.position = pos;
                    yield return null;
                }
    }

    public void StartTransitionAnimation()
    {
        Debug.Log("Starting Camera Movement");
        Camera cam = this.GetComponent<Camera>(); cam.fieldOfView = 73.4f;
        enabled = false;
        MoveCameraToTop move = GetComponent<MoveCameraToTop>();
        move.enabled = true;
    }
}
