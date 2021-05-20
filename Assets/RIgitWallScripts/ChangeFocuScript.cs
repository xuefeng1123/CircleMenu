using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFocuScript : MonoBehaviour, IMixedRealityPointerHandler
{



    //给按钮加tag
    //给按钮加脚本

    string focusTag = "GroundTruth";
    // Start is called before the first frame update
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        GameObject focu = eventData.Pointer.Result.CurrentPointerTarget;
        if(focu.tag == focusTag)
        {
            GameObject.Find("Manager").GetComponent<AdjustPositionScript>().currGroundTruth = focu;
        }
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
