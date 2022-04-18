using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathway : MonoBehaviour
{

    //Allows use of the PathwayScriptableObject
    public PathwayScriptableObject path;
    //Nodes used for the edge building algorithm
    public NodeScriptableObject startNode;
    public NodeScriptableObject currentNode;
    public NodeScriptableObject nextNode;
    public NodeScriptableObject endNode;
    private int itr;
    private bool finished;
    //Asthetics
    public Color color1 = Color.cyan;
    public Color color2 = Color.white;
    private float animationDuration = 5f;



    //Get the next node in the path. assistant function to the buildEdge function 
    void getNext()
    {
        //get next 
        if(itr < path.pathway.Length - 1){
            currentNode = nextNode;
            nextNode = path.pathway[itr + 1];
            itr++;
        }
        //reached the end of the path. set a key to prevent further object generation
        else
        {
            finished = true;
        }
    }


    //Edge building algorithm. takes a array of node and iterativly builds edges based on the path.
    void buildEdge(NodeScriptableObject currentNode, NodeScriptableObject nextNode)
    {
        //If both towers are functional then create the edge
        /*        if(isFunctional(currentNode, nextNode)){
            GameObject g = new GameObject(itr.ToString());
            g.AddComponent<LineRenderer>();
            LineRenderer edge = g.GetComponent<LineRenderer>();
            edge.SetPosition(0, currentNode.location);
            edge.SetPosition(1, nextNode.location);
            //edge.SetWidth();
            //edge.startColor(color1);
            edge.startWidth = 10;
            //edge.endColor(color2);
            Debug.Log("Built Edge");                                        //Debug
            getNext();
        }*/
        if(isFunctional(currentNode, nextNode)){
            
            GameObject g = new GameObject(itr.ToString());
            g.AddComponent<LineRenderer>();
            LineRenderer edge = g.GetComponent<LineRenderer>();
            edge.SetPosition(0, currentNode.location);
            edge.SetPosition(1, nextNode.location);
            //edge.SetWidth();
            //edge.startColor(color1);
            
            edge.startWidth = 10;
            StartCoroutine(AnimateLine(edge));
            //edge.endColor(color2);
            Debug.Log("Built Edge");                                        //Debug
            getNext();
        }
        else{
            //runBackup route
        }
    }

    private IEnumerator AnimateLine(LineRenderer edge){
        float startTime = Time.time;
        Vector3 startPos = edge.GetPosition(0);
        Vector3 endPos = edge.GetPosition(1);
        Vector3 pos = startPos;
            while(pos != endPos)
            {
                float t = (Time.time - startTime) / animationDuration;
                pos = Vector3.Lerp(startPos, endPos, t);
                edge.SetPosition(1,pos);
                yield return null;
            }      
    }  


    //Checks if both nodes in a edge are functional. used to test if a tower is operational or not
    //used to check if buildEdge should be run
    bool isFunctional(NodeScriptableObject currentNode, NodeScriptableObject nextNode)
    {
        //True case
        if(currentNode.getFunctional() & nextNode.getFunctional()) {
            return true;
        }
        //False case
        else{
            return false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //instantiate objects
        startNode = path.pathway[0];
        endNode = path.pathway[path.pathway.Length - 1];
        itr = 1;
        currentNode = startNode;
        nextNode = path.pathway[itr];
    }

    // Update is called once per frame
    void Update()
    {

        //temporary for testing the edge builder replace once UI gets implemented
        if(!finished) {
            if(Input.GetMouseButton(0)) {
                buildEdge(currentNode, nextNode);
               // StartCoroutine(buildEdge(currentNode, nextNode));
            }  
        }

        if(finished){
            Debug.Log("reached the truncking location");
        }

    }


}
