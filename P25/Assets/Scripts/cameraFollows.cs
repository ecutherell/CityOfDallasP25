using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollows : MonoBehaviour
{
    public GameObject target;
    private Vector3 cameraPosition;
    private Vector3 offset;
    private GameObject parentCam;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position - target.transform.position + (0,1,2);
        //Quaternion rotation = Quaternion.Euler(0, 30, 0);
        offset = new Vector3(100,40,0);
        //parentCam = GetComponent<Camera>();
        this.transform.Rotate(30f,-90f,0f,0f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        target = GameObject.Find("Sphere");
      
        //target = GameObject.Find(itr.toString());
        //offset = new Vector3(target.transform.position.x, target.transform.position.y+2, target.transform.position.z);
        cameraPosition = target.transform.position + offset;
        transform.position = cameraPosition;

    }
}
