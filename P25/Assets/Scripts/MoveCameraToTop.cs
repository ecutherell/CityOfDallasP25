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
        Camera cam = this.GetComponent<Camera>(); cam.fieldOfView = 70;
        StartCoroutine(GoToTop());
    }

        private IEnumerator GoToTop()
    {

                float startTime = Time.time;
                Vector3 startPos = this.transform.position;
                Vector3 endPos = TopCamera.transform.position;
                Quaternion startRotation = this.transform.rotation ;
                Quaternion endRotation = Quaternion.Euler(new Vector3(90,180,0));
                Vector3 pos = startPos;
                float  animationDuration = 2f;

                while(pos != endPos)
                {
                    float t = (Time.time - startTime) / animationDuration;
                    pos = Vector3.Lerp(startPos, endPos, t);
                    this.transform.rotation = Quaternion.Lerp( startRotation, endRotation, t) ;
                    this.transform.position = pos;
                    yield return null;
                }
    }
}
