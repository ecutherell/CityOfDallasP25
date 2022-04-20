using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMovingScript : MonoBehaviour
{
    public PathwayScriptableObject path;
    //Nodes used for the edge building algorithm
    public NodeScriptableObject startNode;
    public NodeScriptableObject currentNode;
    public NodeScriptableObject nextNode;
    public NodeScriptableObject endNode;
    public float speed = 2.5f;
    private int itr;
    // Start is called before the first frame update
    void Start()
    {
        startNode = path.pathway[0];
        endNode = path.pathway[path.pathway.Length - 1];
        itr = 1;
        currentNode = startNode;
        nextNode = path.pathway[itr];        
    }

    // Update is called once per frame
    void Update()
    {
        MoveNode(currentNode, nextNode);
    }

        void MoveNode(NodeScriptableObject currentNode,NodeScriptableObject nextNode)
    {
        Debug.Log("Moving Node");
        int index = 0;
        
        //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        gameObject.transform.position = currentNode.location;
        gameObject.transform.localScale += new Vector3(20f,20f,20f);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nextNode.location, speed * Time.deltaTime);
        // if(sphere.transform.position == getLinePositions(edge)[index])
        //     index+=1;
        // else if(index == getLinePositions(edge).Length)
        //     index = 0;
        



    }
}
