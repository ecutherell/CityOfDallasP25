using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pathway", menuName = "Pathway")]
public class PathwayScriptableObject : ScriptableObject
{
    public NodeScriptableObject[] pathway;      //Array of nodes that represent a radio pathway
                                                //Pathway ends at a trunking location. (Node 0 or Node 28)

    //This project uses two paths
    //Main: 14 -> 8 -> 4 -> 2 -> 0
    //Backup: 14 -> 20 -> 19 -> 18 -> 25 -> 28
}
