using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Node script. attach to all the node gameobjects in the scene
 *
 */

public class Node : MonoBehaviour
{
    //gives unity a way to attach the script to a object
    public NodeScriptableObject node;
    
    //Visual FX. 
    public ParticleSystem lightParticle;
    public ParticleSystem signalParticle;
    public Color currentColor;
    //manages emissions. allow emmit to be enabled once per scene
    private bool emissionManager;

    
    void Awake()
    {
        //move node to position on wake up
        this.transform.position = node.location;
        //set the scene
        node.transmitted = false;
        //turn off emmissions until the tower transmits
        emissionManager = false;
        var em = signalParticle.emission;
        em.enabled = false;
    }

    //Handles coloring of the particle effects. makes the tower status visually clear
    //red = nonfunctional
    //blue = functional and ready to transmit
    //green = transmiting trunked signal
    private void ColorManager()
    {
        //Tower Nonfunctional
        if(node.functional == false) 
        {
            currentColor = Color.red;
            ParticleSystem.MainModule lp = lightParticle.main;
            lp.startColor = currentColor;
        }
        //Tower Functioning and relaying to trunking location
        if(node.functional & node.transmitted == false)
        {
            currentColor = Color.cyan;
            ParticleSystem.MainModule lp = lightParticle.main;
            lp.startColor = currentColor;
        }
        //Tower Functioning and transmitting trunked message
        if(node.transmitted) 
        {
            currentColor = Color.green;
            ParticleSystem.MainModule sp = signalParticle.main;
            ParticleSystem.MainModule lp = lightParticle.main;
            lp.startColor = currentColor;
            
            //sp.startColor = currentColor;         // only if we want sonar particles to be green. comment out for white

            //turn on emmision. locked behind conditional so update doesn't re-emmit constantly
            if(emissionManager == false)
            {
                var em = signalParticle.emission;
                em.enabled = true;
                emissionManager = true;
            }

        }
    }

    void FixedUpdate()
    {
        ColorManager();
    }
    
    
}