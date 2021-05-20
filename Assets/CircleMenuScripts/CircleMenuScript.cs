using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMenuScript : MonoBehaviour
{
    static string circleMenuName = "circleMenu";

    public GameObject circleMenu;
    // Start is called before the first frame update
    void Start()
    {
        circleMenu = GameObject.Find(circleMenuName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFocus()
    {
        circleMenu.SetActive(true);
    }
    public void OnFocusOff()
    {
        circleMenu.SetActive(false);
    }
}
