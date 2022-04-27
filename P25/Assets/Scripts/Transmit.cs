using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Transmit : MonoBehaviour
{

    //Declare variables
    public Pathway p;

    //This function simulates the signals transmition through all towers after reaching the trunking location
    //A modified BFS (breadth first search) is used to build signals between adjacent towers
    public void transmit(NodeScriptableObject root)
    {

        Debug.Log("inside transmit");                                                                        //DEBUG
        Debug.Log("Name of root: " + root.nodeName);                                                         //DEBUG

        root.transmitted = true;
        Debug.Log("Dequing: " + root.nodeName);                                                                 //DEBUG

        foreach(NodeScriptableObject v in root.linkedNodes)
        {
            if(v.transmitted)
            {
                continue;
            }
            
            StartCoroutine(AnimateTransmition(root,v));

            Debug.Log("Enquing: " + v.nodeName);
        }
    }

    //This function simulates the signals transmition through in realtime
    //A modified BFS (breadth first search) is used to build signals between adjacent towers
    public void RealtimeTransmit(NodeScriptableObject root)
    {
        //list for use in the BFS enqueue the root 

        Debug.Log("inside transmit");                                                                        //DEBUG
        Debug.Log("Name of root: " + root.nodeName);                                                         //DEBUG

        //Stop previous corutine
        StopAllCoroutines();

        //Create data structures to use in BFS
        Queue<NodeScriptableObject> list = new Queue<NodeScriptableObject>();
        list.Enqueue(root);

        //begin the BFS
        while(list.Any())                       //continue looping until the list is empty
        {

            Debug.Log("inside BFS");                                                                         //DEBUG

            NodeScriptableObject u = list.Dequeue();
            u.transmitted = true;

            Debug.Log("Dequing: " + u.nodeName);                                                             //DEBUG

            foreach(NodeScriptableObject v in u.linkedNodes)
            {

                if(v.transmitted) {
                    continue;
                }

                Debug.Log("animating: " + u.nodeName + " " + v.nodeName);
                StartCoroutine(AnimateTransmition(u,v));
                list.Enqueue(v);

                Debug.Log("Enquing: " + v.nodeName);
            }
        }
    }


    //Edge building algorithm. takes a array of node and iterativly builds edges based on the path.
    LineRenderer buildEdgeTransmit(NodeScriptableObject currentNode, NodeScriptableObject nextNode)
    {
        if(p.isFunctional(currentNode, nextNode)){
            
            GameObject g = new GameObject();
            g.AddComponent<LineRenderer>();
            LineRenderer edge = g.GetComponent<LineRenderer>();
            edge.SetPosition(0, currentNode.location);
            edge.SetPosition(1, nextNode.location);
            edge.material = p.customMat;
            edge.startColor = p.color2;
            edge.endColor = p.color2;

            edge.startWidth = 20;

            return edge;
        }
        else{
            //runBackup route
            return null;
        }
    }


    //Coroutine for animating the line. continously sets the edge length of the next node for a specified duration 
    private IEnumerator AnimateTransmition(NodeScriptableObject parent, NodeScriptableObject child){

        //build edge 
        LineRenderer edge = buildEdgeTransmit(parent, child);

        //return without interpolating edge building. used for the fast animation
        if(p.realtimeAnimate) yield break;

        //get positions needed for the animation
        float startTime = Time.time;
        Vector3 startPos = edge.GetPosition(0);
        Vector3 endPos = edge.GetPosition(1);
        Vector3 pos = startPos;

        while(pos != endPos)
        {
            float t = (Time.time - startTime) / p.animationDuration;
            pos = Vector3.Lerp(startPos, endPos, t);
            edge.SetPosition(1,pos);
            yield return null;
        }

        transmit(child);

        yield return new WaitForSeconds(0.2f);
    }
      
}
