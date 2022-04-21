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
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        target = GameObject.Find("Sphere");
        cameraPosition = target.transform.position + offset;
        transform.position = cameraPosition;

    }
}
