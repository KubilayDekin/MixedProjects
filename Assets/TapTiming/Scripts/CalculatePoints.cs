using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculatePoints : MonoBehaviour
{
    public Text scoreText;
    public Text informationText;

    private float timer;

    private void Start()
    {
        timer = 1.5f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (timer > .5f)
            {
                Calculate();
                informationText.text = "";
                timer = 0;
            }
            else
            {
                informationText.text = "You can click 1 time in every 0.5 seconds !";
            }
        }
    }

    private void Calculate()
    {
        float distance= 3.3f-Mathf.Abs(transform.localPosition.x);
        float points = (100 / (3.3f))*distance;

        scoreText.text = ""+Mathf.Round(points);
    }
}
