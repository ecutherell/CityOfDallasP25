using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimationSpeedAdjuster : MonoBehaviour
{
     public GameObject holder;
     Pathway pathobj;
     public Slider parentSlider;
    void Start()
    {
          pathobj = holder.GetComponent<Pathway>();
    }

    // Update is called once per frame
    void Update()
    {
            //Debug.Log(parentSlider.value);
            pathobj.AdjustAnimationDuration(parentSlider.value);

    }
}
