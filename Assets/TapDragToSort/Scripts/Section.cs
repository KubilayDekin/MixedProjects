using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    public List<GameObject> objectsOnSection;
    public GameObject[] subsections;

    public void CheckOrder()
    {
        string tempTag;
        bool isOrdered=false;

        tempTag = subsections[0].GetComponent<Subsection>().objectOnSection.tag;

        for(int i = 1; i < 4; i++)
        {
            if (tempTag == subsections[i].GetComponent<Subsection>().objectOnSection.tag)
            {
                isOrdered = true;
            }
            else
            {
                isOrdered = false;
                break;
            }
        }
        if (isOrdered)
        {
            gameObject.SetActive(false);
            TDTS_Manager.tDTS.CheckWin();
        }
    }
}
