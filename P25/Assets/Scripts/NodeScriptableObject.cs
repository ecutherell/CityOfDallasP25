using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Node", menuName = "Node")]
public class NodeScriptableObject : ScriptableObject
{
    //Scriptable object holding the attributes for the nodes.

    public string nodeName;                             //Name of the tower the node belongs to
    public int id;                                      //Number ID of the node. used for animation script
    public NodeScriptableObject[] linkedNodes;          //ID's of nodes this tower links to
    public bool functional;                             //Determines if the tower is functional or not
    public Vector3 location;                            //location of the node in 3d space

    //getters
    public bool getFunctional(){
        return functional;
    }

    public Vector3 getLocation(){
        return location;
    }

}
