using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operation : MonoBehaviour
{
    // Start is called before the first frame update
    public string name;
    public string icon;

    public Vector3 localPosition;

    public Operation(string name, string icon)
    {
        this.name = name;
        this.icon = icon;
    }

    public void setOperation(Operation operation)
    {
        this.name = operation.name;
        this.icon = operation.icon;
    }
}
