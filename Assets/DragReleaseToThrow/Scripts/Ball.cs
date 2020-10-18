using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Win Collider")
        {
            GameManager.gM.Reset(1);
        }
        else if (other.name == "Destroyer Collider")
        {
            GameManager.gM.Reset(-1);
        }
    }
}
