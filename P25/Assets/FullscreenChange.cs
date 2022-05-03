using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenChange : MonoBehaviour
{
    // Start is called before the first frame update
    private bool fullScreen;
    void Start()
    {
        fullScreen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F11) && !fullScreen)
        {
            Screen.fullScreen = !Screen.fullScreen;
              //Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
        }
        else{
            Screen.fullScreen = Screen.fullScreen;
            //fullScreen = true;
            //Screen.fullScreenMode = FullScreenMode.Windowed;
        }

    }
}
