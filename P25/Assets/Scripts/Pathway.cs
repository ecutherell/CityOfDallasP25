using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pathway : MonoBehaviour
{

    //Allows use of the PathwayScriptableObject
    public PathwayScriptableObject path;
    //Nodes used for the edge building algorithm
    public NodeScriptableObject startNode;
    public NodeScriptableObject currentNode;
    public NodeScriptableObject nextNode;
    public NodeScriptableObject endNode;
    public int itr;
    private bool finished;
    private bool autoAnimate;
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
    LineRenderer buildEdge(NodeScriptableObject currentNode, NodeScriptableObject nextNode)
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
            edge.material = new Material(Shader.Find("Sprites/Default"));
            edge.startColor = color1;
            edge.endColor = color2;

            edge.startWidth = 20;
            //StartCoroutine(AnimateLine(edge));
            //Debug.Log("Built Edge");                                        //Debug
            //getNext();

            return edge;
        }
        else{
            //runBackup route
            return null;
        }
    }


    //Coroutine for animating the line. continously sets the edge length of the next node for a specified duration 
    private IEnumerator AnimateLine(){

        while(!finished){

            if(autoAnimate) {

                //build edge 
                LineRenderer edge = buildEdge(currentNode, nextNode);
               // GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);


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
                    //sphere.transform.position = pos;
                    yield return null;
                } 
                // pos = startPos;
                // sphere.transform.localScale = new Vector3(10f,10f,10f);
                // sphere.transform.position = edge.GetPosition(0);
                // while(pos != endPos)
                // {
                //     float t = (Time.time - startTime) / animationDuration;
                //     pos = Vector3.Lerp(startPos, endPos, t);
                //     sphere.transform.position = pos;
                //     yield return null;
                // }
            
                getNext();
            }
            else if(Input.GetMouseButton(0))
            {
                LineRenderer edge = buildEdge(currentNode, nextNode);
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.localScale = new Vector3(10f,10f,10f);
                sphere.transform.position = edge.GetPosition(0);

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
                // pos = startPos;
                // sphere.transform.localScale = new Vector3(10f,10f,10f);
                // sphere.transform.position = edge.GetPosition(0);
                // while(pos != endPos)
                // {
                //     float t = (Time.time - startTime) / animationDuration;
                //     pos = Vector3.Lerp(startPos, endPos, t);
                //     sphere.transform.position = pos;
                //     yield return null;
                // }
            
                getNext();
            }



             yield return new WaitForEndOfFrame();
        }
    }  

    private IEnumerator AnimateLightNode()
    {
         while(!finished){

            if(Input.GetMouseButton(0) | autoAnimate) {

                //build edge 
                LineRenderer edge = buildEdge(currentNode, nextNode);
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.localScale = new Vector3(10f,10f,10f);
                sphere.transform.position = edge.GetPosition(0);

                //get positions needed for the animation
                float startTime = Time.time;
                Vector3 startPos = edge.GetPosition(0);
                Vector3 endPos = edge.GetPosition(1);
                Vector3 pos = startPos;

                while(pos != endPos)
                {
                    float t = (Time.time - startTime) / animationDuration;
                    pos = Vector3.Lerp(startPos, endPos, t);
                    sphere.transform.position = pos;
                    yield return null;
                } 
            
                getNext();
            }

             yield return new WaitForEndOfFrame();       
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
        finished = false;
        autoAnimate = false;

        //Run the Coroutine
        //StartCoroutine(AnimateLine());
    }

    public void startAnimation()
    {
        //autoAnimate = true;
        StartCoroutine(AnimateLine());
       // StartCoroutine(AnimateLightNode());
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
         //SceneManager.LoadScene("SampleScene");
        
    }

    // Update is called once per frame
    /*void Update()
    {

    }
    */
}

