using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subsection : MonoBehaviour
{
    public bool isEmpty;
    public GameObject objectOnSection;

    private bool dragging = false;
    private float distance;
    private Vector3 startDist;
    private float startY;

    private void Start()
    {
        isEmpty = true;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material = TDTS_Manager.tDTS.defaultMat;
    }

    void OnMouseDown()
    {
        if (objectOnSection)
        {
            startY = objectOnSection.transform.position.y;
            TDTS_Manager.tDTS.isDragging = true;
            TDTS_Manager.tDTS.fromSection = gameObject;
            distance = Vector3.Distance(objectOnSection.transform.position, Camera.main.transform.position);
            dragging = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            startDist = objectOnSection.transform.position - rayPoint;
        }
    }

    void OnMouseUp()
    {
        if (objectOnSection)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                TDTS_Manager.tDTS.toSection = hit.collider.gameObject;
            }

            dragging = false;

            if (hit.collider && hit.collider.gameObject.GetComponent<Subsection>().isEmpty)
            {

                objectOnSection.transform.position = new Vector3(hit.collider.gameObject.transform.position.x,
                                                                 startY,
                                                                 hit.collider.gameObject.transform.position.z);


                hit.collider.gameObject.GetComponent<Subsection>().objectOnSection = objectOnSection;
                hit.collider.gameObject.GetComponent<Subsection>().objectOnSection.transform.parent = hit.collider.gameObject.transform;
                hit.collider.gameObject.GetComponent<Subsection>().isEmpty = false;
                isEmpty = true;
                objectOnSection = null;

                bool isFull=false;

                for(int i = 0; i < 4; i++)
                {
                    if (hit.collider.gameObject.transform.parent.GetComponent<Section>().subsections[i].GetComponent<Subsection>().objectOnSection)
                    {
                        isFull = true;
                    }
                    else
                    {
                        isFull = false;
                        break;
                    }
                }
                if (isFull)
                {
                    hit.transform.parent.GetComponent<Section>().CheckOrder();
                }

            }
            else
            {
                objectOnSection.transform.position = new Vector3(transform.position.x , startY , transform.position.z);               
            }

            TDTS_Manager.tDTS.isDragging = false;

        }
    }
    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);

            objectOnSection.transform.position = rayPoint + startDist;
            objectOnSection.transform.position = new Vector3(objectOnSection.transform.position.x,
                                                             20f,
                                                             objectOnSection.transform.position.z);
        }
    }
}
