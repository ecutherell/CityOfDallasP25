using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInitialize : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 offset = new Vector3(112,0,-5);
    public Pathway startTower;
    public Vector3 startTowerLocation;
    void Start()
    {
        
        //startTowerLocation = startTower.startNode.location;
        //yield return new WaitForSeconds(3);
        //StartCoroutine(moveCamera());
        
    }

    public void moveCamera()
    {
        //yield return new WaitForSeconds(1);
        //Debug.Log(startTower.startNode.nodeName);
        startTower = GameObject.Find("Path Object").GetComponent<Pathway>();
        this.transform.position = startTower.startNode.location + offset;
    }

    // Update is called once per frame
    // void Update()
    //     {
    //         Debug.Log(startTower.startNode.nodeName);
    //     }

}
