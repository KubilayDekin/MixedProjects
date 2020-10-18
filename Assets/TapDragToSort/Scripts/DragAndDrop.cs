using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool dragging = false;
    private float distance;
    private Vector3 startDist;
    private float startY;

    private void Start()
    {
        startY = transform.position.y;
    }
    void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        startDist = transform.position - rayPoint;
    }

    void OnMouseUp()
    {
        dragging = false;
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
    }

    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint + startDist;
            transform.position = new Vector3(transform.position.x, 20f, transform.position.z);
        }
    }
}
