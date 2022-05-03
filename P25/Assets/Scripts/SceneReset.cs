using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneReset : MonoBehaviour
{
    // Start is called before the first frame update
    public Button yourButton;
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(ClickEvent);
    }

     void ClickEvent()
     {
          SceneManager.LoadScene("SampleScene");
     }
}
