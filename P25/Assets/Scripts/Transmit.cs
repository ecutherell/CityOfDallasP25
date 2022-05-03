using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEditor;
public class Transmit : MonoBehaviour
{

    //Declare variables
    public Pathway p ;
    //This function simulates the signals transmition through all towers after reaching the trunking location
    //A modified BFS (breadth first search) is used to build signals between adjacent towers
    public void transmit(NodeScriptableObject root)
    {

        

        Debug.Log("inside transmit");                                                                        //DEBUG
        Debug.Log("Name of root: " + root.nodeName);                                                         //DEBUG
        /*Future people can maybe get this coroutine to work and generate text labels for the towers during top down view :D*/
        //StartCoroutine(generateUINames(root));
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



/*private IEnumerator generateUINames(NodeScriptableObject parent)
{
    int Layer = LayerMask.NameToLayer("UI");
    GameObject canvas = GameObject.Find("DisplayTowerNames");
    GameObject TextObject = new GameObject(parent.nodeName, typeof(Text));
    TextObject.layer = Layer;
    Text myText = TextObject.GetComponent<Text>();

    TextObject.transform.SetParent(canvas.transform);
    RectTransform rect = TextObject.GetComponent<RectTransform>();
    //newX and newY are SUPER scuffed, I was trying to get an approximate UI location for the tower labels during the final transmission and did some quirky math on 3 values to get the average
    //and thats how I got these float numbers, there's probably a better way but it's 1:32am and my brain is empty
    ///Also the math was basically divide node x by a position that i manually moved and rinsed and repeated
    float newX = 0f;// = parent.location.x * 0.0112f;
    float newY = 0f;// = Mathf.Abs(parent.location.z * 0.0231f);
    //rect.Rotate(180f,0,-180f,0);
    //rect.
    //rect.anchoredPosition3D.z = parent.location.z;
    Debug.Log(rect.anchoredPosition3D);

    //These if statements just determine whether the UI's x or y is positive or negative, the UI does not use Z as its a 2D plane
    if(parent.location.x > 0 && parent.location.z < 0) //top left
    {
     newX = parent.location.x * 0.0112f;
     newY = Mathf.Abs(parent.location.z * 0.0231f);
    }

    else if(parent.location.x < 0 && parent.location.z < 0) //top right
    {
     newX = parent.location.x * 0.0112f;
     newY = Mathf.Abs(parent.location.z * 0.0231f);   
    }
    else if(parent.location.x > 0 && parent.location.z > 0) //bottom left
    {
     newX = parent.location.x * 0.0112f;
     newY = parent.location.z * 0.0231f * -1;   
    }
    else if(parent.location.x < 0 && parent.location.z > 0) //bottom left
    {
     newX = parent.location.x * 0.0112f;
     newY = parent.location.z * 0.0231f * -1;   
    }
    //rect.anchoredPosition3D =// parent.location  new Vector3(newX,newY,0);
    rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
    rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 250);

    /*This sh*t doesn't work when trying to build, will have to do Resources.Load() later but idrc right now
    //myText.font = (Font)AssetDatabase.LoadAssetAtPath("Assets/TextMesh Pro/Fonts/LiberationSans.ttf", typeof(Font));
    myText.font = Resources.Load("Assets/Resources/Fonts/LiberationSans.ttf", typeof(Font));
    myText.fontSize = 24;
    myText.verticalOverflow = VerticalWrapMode.Overflow; /*Literally if you do not have this the text will not show up...took me forever to figure this out
    myText.text = parent.nodeName;
    myText.transform.Rotate(90f,180f,0,0); /*Rotates the text to be flat and aligned with the canvas plane
   // myText.transform.position = new Vector3(0,0,0);

     //myText = canvas.AddComponent<GameObject>();
   // myText.text = parent.nodeName;
    yield return null;
}
*/

    //Coroutine for animating the line. continously sets the edge length of the next node for a specified duration 
    private IEnumerator AnimateTransmition(NodeScriptableObject parent, NodeScriptableObject child){

        //build edge 
        LineRenderer edge = buildEdgeTransmit(parent, child);
        edge.widthMultiplier = 10f;

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

        yield return new WaitForSeconds(0.1f);
    }
      
}
