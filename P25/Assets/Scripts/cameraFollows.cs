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
    public Pathway p;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position - target.transform.position + (0,1,2);
        //Quaternion rotation = Quaternion.Euler(0, 30, 0);
        startTower = GameObject.Find("Path Object").GetComponent<Pathway>();
        offset = new Vector3(350,30,0);
        //this.transform.position = startTower.startNode.location + offset;
        
        TopCamera = GameObject.Find("TopDownCamera");
        //parentCam = GetComponent<Camera>();
        this.transform.Rotate(30f,-90f,0f,0f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //only use the tracking camera if in slowed down animation
        if(p.realtimeAnimate == false)
        {
            target = GameObject.Find("Sphere");
        
            //target = GameObject.Find(itr.toString());
            //offset = new Vector3(target.transform.position.x, target.transform.position.y+2, target.transform.position.z);
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
                    //edge.SetPosition(1,pos);
                    //sphere.transform.position = pos;
                    yield return null;
                }
    }

    public void StartTransitionAnimation()
    {
        Debug.Log("Starting Camera Movement");
        enabled = false;
        MoveCameraToTop move = GetComponent<MoveCameraToTop>();
        move.enabled = true;
       // StartCoroutine(GoToTop());
    }
}
