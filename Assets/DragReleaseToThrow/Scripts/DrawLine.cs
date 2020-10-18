using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public LineRenderer lr;

    private void Start()
    {
        InvokeRepeating("Draw", 0, 0.15f);
    }
    private void Draw()
    {
        lr.positionCount++;
        lr.SetPosition(lr.positionCount-1,transform.position);
        transform.hasChanged = false;
    }

}
