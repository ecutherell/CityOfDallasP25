using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNodeScript : MonoBehaviour
{
    public PathwayScriptableObject path;
    //Nodes used for the edge building algorithm
    public NodeScriptableObject startNode;
    public NodeScriptableObject currentNode;
    public NodeScriptableObject nextNode;
    public NodeScriptableObject endNode;
    public int itr = 0;
    public float speed = 2.4f;
    
    // Start is called before the first frame update
    void Start()
    {
        startNode = path.pathway[0];
        endNode = path.pathway[path.pathway.Length - 1];
        itr = 1;
        currentNode = startNode;
        nextNode = path.pathway[itr];
        
        gameObject.transform.position = currentNode.location;
    }

    // Update is called once per frame
    void Update()
    {
       // MoveNode();
    }

    void MoveNode(){
          //  Debug.Log("Moving Node");
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nextNode.location, speed * Time.deltaTime);
    }
}
