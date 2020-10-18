using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBall : MonoBehaviour
{
    void Update()
    {
        float x = -Input.GetAxis("Horizontal") * 50f * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * 50f * Time.deltaTime;

        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Rotate(0, x, 0, Space.World);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            transform.Rotate(y, 0, 0, Space.World);
        }
    }
}
