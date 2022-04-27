using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraToTop : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject TopCamera;
    void Start()
    {
        
        TopCamera = GameObject.Find("TopDownCamera");
        //this.transform.Rotate(-30f,90f,0f,0f);
        StartCoroutine(GoToTop());
    }

        private IEnumerator GoToTop()
    {

                float startTime = Time.time;
                Vector3 startPos = this.transform.position;
                Vector3 endPos = TopCamera.transform.position;
                Quaternion startRotation = this.transform.rotation ;
                Quaternion endRotation = Quaternion.Euler(new Vector3(90,180,0));//Quaternion.Euler( new Vector3(30,-90,0) ) * startRotation ;
                Vector3 pos = startPos;
                float  animationDuration = 5f;

                while(pos != endPos)
                {
                    float t = (Time.time - startTime) / animationDuration;
                    pos = Vector3.Lerp(startPos, endPos, t);
                    this.transform.rotation = Quaternion.Lerp( startRotation, endRotation, t) ;
                    this.transform.position = pos;
                    //edge.SetPosition(1,pos);
                    //sphere.transform.position = pos;
                    yield return null;
                }
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
