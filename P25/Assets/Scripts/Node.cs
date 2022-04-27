using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    //gives unity a way to attach the script to a object
    public NodeScriptableObject node;
    
    public ParticleSystem lightParticle;
    public ParticleSystem signalParticle;
    public Color currentColor;

    
    void Awake()
    {
        this.transform.position = node.location;
        node.transmitted = false;
        var em = signalParticle.emission;
        em.enabled = false;
    }

    
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
            var em = signalParticle.emission;
            em.enabled = true;
        }

    }

    void Update()
    {
        ColorManager();
    }
    
    
}
