using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionButtonsScript : MonoBehaviour, IMixedRealityPointerHandler
{

    static public string upButtonName = "upButton";
    static public string downButtonName = "downButton";
    static public string leftButtonName = "leftButton";
    static public string rightButtonName = "rightButton";
    static public string forwardButtonName = "forwardButton";
    static public string backButtonName = "backButton";
    static public float offset = 0.005f;


    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        GameObject button = eventData.Pointer.Result.CurrentPointerTarget;
        Vector3 offsetVector = Vector3.zero;
        if(button.name == upButtonName)
        {
            offsetVector = new Vector3(0, offset, 0);
        }
        else if(button.name == downButtonName)
        {
            offsetVector = new Vector3(0, -offset, 0);
        }
        else if(button.name == leftButtonName)
        {
            offsetVector = new Vector3(-offset, 0, 0);
        }
        else if(button.name == rightButtonName)
        {
            offsetVector = new Vector3(offset, 0, 0);
        }
        else if (button.name == forwardButtonName)
        {
            offsetVector = new Vector3(0, 0, offset);
        }
        else if (button.name == backButtonName)
        {
            offsetVector = new Vector3(0, 0, -offset);
        }
        GameObject.Find("Manager").GetComponent<AdjustPositionScript>().currGroundTruth.transform.position += offsetVector;
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
    }

    
}
