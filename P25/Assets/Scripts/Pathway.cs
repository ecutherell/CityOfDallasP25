using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Pathway : MonoBehaviour
{

    //Allows use of the PathwayScriptableObject
    public PathwayScriptableObject path;
    //Nodes used for the edge building algorithm
    public NodeScriptableObject startNode;
    public NodeScriptableObject currentNode;
    public NodeScriptableObject nextNode;
    public NodeScriptableObject endNode;
    //Transmition script reference
    public Transmit transmitter;
    //helper variables
    public int itr;
    private bool finished;
    //Control variables
    private bool autoAnimate;
    public bool realtimeAnimate;
    public float animationDuration = 5f;
    //Asthetics
    public Material customMat;
    public Color color1;
    public Color color2;



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
    LineRenderer buildEdge(NodeScriptableObject currentNode, NodeScriptableObject nextNode)
    {
        /*This commented out section is for having text near towers in dop down view, future people can uncomment this and work with Transmit.cs to figure out how to do it*/
        // GameObject currentText = GameObject.Find("CurrentNodeText");
        // //currentText.transform.parent.gameObject.SetActive(true);// = true;
        // GameObject nextText = GameObject.Find("NextNodeText");
        // TextMeshProUGUI CurrentGUIText = currentText.GetComponent<TextMeshProUGUI>();
        // TextMeshProUGUI NextGUIText = nextText.GetComponent<TextMeshProUGUI>();
        // CurrentGUIText.text = currentNode.nodeName;
        // NextGUIText.text = nextNode.nodeName;
        if(isFunctional(currentNode, nextNode)){
            
            GameObject g = new GameObject(itr.ToString());
            g.AddComponent<LineRenderer>();
            LineRenderer edge = g.GetComponent<LineRenderer>();
            edge.SetPosition(0, currentNode.location);
            edge.SetPosition(1, nextNode.location);
            edge.material = customMat;
            edge.startColor = color1;
            edge.endColor = color1;

            edge.startWidth = 20;

            return edge;
        }
        else{
            //runBackup route
            return null;
        }
    }


    //Coroutine for animating the line. continously sets the edge length of the next node for a specified duration 
    private IEnumerator AnimateLine(){

        //if statement that is controlled by the realtimeAnimate variable.
        //if building in realtime we can skip this part and call the realtimeTransmit funciton
        while(realtimeAnimate)
        {
            yield return new WaitUntil(() => (autoAnimate | Input.GetMouseButton(0))); 
            transmitter.RealtimeTransmit(endNode);
            yield break;
        }

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        GameObject camera = GameObject.Find("EdgeFollow");
        cameraFollows camScript = camera.GetComponent<cameraFollows>();
        CameraInitialize camScript2 = camera.GetComponent<CameraInitialize>();
        camera.SetActive(true);
        camScript.StartTracking();
        MeshRenderer mesh = sphere.GetComponent<MeshRenderer>();
        mesh.enabled = false;
        
        while(!finished){

            if(autoAnimate | Input.GetMouseButton(0)) {
        
        camScript.enabled = true;
        camScript2.enabled = false;

                //build edge 
                LineRenderer edge = buildEdge(currentNode, nextNode);
                edge.widthMultiplier = 10f;
                edge.numCapVertices = 5;
                
                sphere.transform.localScale = new Vector3(10f,10f,10f);
                //get positions needed for the animation
                float startTime = Time.time;
                Vector3 startPos = edge.GetPosition(0);
                Vector3 endPos = edge.GetPosition(1);
                Vector3 pos = startPos;

                while(pos != endPos)
                {
                    float t = (Time.time - startTime) / animationDuration;
                    pos = Vector3.Lerp(startPos, endPos, t);
                    edge.SetPosition(1,pos);
                    sphere.transform.position = pos;
                    yield return null;
                }

                getNext();
            }

            //yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => (autoAnimate | Input.GetMouseButton(0))); 
        }

        //clear path and produce outgoing transmition
        Debug.Log("at end of corutine");
        yield return new WaitForSeconds(1);
        ClearPath();

        camScript.StartTransitionAnimation();
        transmitter.transmit(endNode);
    }  


    //Checks if both nodes in a edge are functional. used to test if a tower is operational or not
    //used to check if buildEdge should be run
    public bool isFunctional(NodeScriptableObject currentNode, NodeScriptableObject nextNode)
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
    /*This is a good function for testing, so do not delete it*/
    // void Start()
    // {
    //     //instantiate objects
    //     startNode = path.pathway[0];
    //     endNode = path.pathway[path.pathway.Length - 1];
    //     itr = 1;
    //     currentNode = startNode;
    //     nextNode = path.pathway[itr];
    //     finished = false;
    //     autoAnimate = false;
    //     realtimeAnimate = false;
    //     color1 = Color.cyan;
    //     color2 = Color.green;

    //     //Run the Coroutine
    //     //StartCoroutine(AnimateLine());
    // }
    public void setPath(){
        startNode = path.pathway[0];
        endNode = path.pathway[path.pathway.Length - 1];
        itr = 1;
        currentNode = startNode;
        nextNode = path.pathway[itr];
        finished = false;
        autoAnimate = false;
        //realtimeAnimate = false;
        color1 = Color.cyan;
        color2 = Color.green;
    }

    public void startAnimation()
    {
        StartCoroutine(AnimateLine());
    }

    public void TurnAutoOn()
    {
        autoAnimate = true;
    }

    public void TurnAutoOff()
    {
        autoAnimate = false;
    }

    public void AdjustAnimationDuration(float num)
    {
        //Debug.Log(num);
        animationDuration = num;
    }

    public void EndAnimation(){
        StopCoroutine(AnimateLine());
        
    }

    public void RTOn()
    {
        realtimeAnimate = true;
    }
    public void RTOff()
    {
        realtimeAnimate = false;
    }

    //Deletes edges created in this script. improves performance
    private void ClearPath()
    {
        for(int i = 1; i <= itr; i++)
        {
            GameObject temp = GameObject.Find(i.ToString());
            Destroy(temp);
        }
    }
}

