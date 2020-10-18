using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    [Header("Gameplay Values")]
    public float power;
    public Vector3 minPower;
    public Vector3 maxPower;
    public GameObject spawnPoint;

    [Header("Rigidbody")]
    public Rigidbody rb;

    private bool controller;

    Vector3 force;
    Vector3 startPoint;
    Vector3 endPoint;
    Camera cam;

    private void Start()
    {
        spawnPoint = GameObject.Find("Spawn Point");
        cam = Camera.main;
        controller = true;
    }

    private void Update()
    {
        if (controller)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPoint = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                endPoint = Input.mousePosition;
                force = new Vector3(Mathf.Clamp(
                    startPoint.x - endPoint.x, minPower.x, maxPower.x),
                    0,
                    Mathf.Clamp(startPoint.y - endPoint.y, minPower.z, maxPower.z));

                rb.AddForce(force * power);
                controller = false;
            }
        }
     
    }

}
