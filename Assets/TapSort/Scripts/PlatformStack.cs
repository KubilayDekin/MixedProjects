using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStack : MonoBehaviour
{
    public Stack<GameObject> platformStack=new Stack<GameObject>(); // stores balls on platform

    public bool isOk; // are balls on platform, placed correctly


    private void Start()
    {
        isOk = false;
    }
    private void Update()
    {
        if (isOk)
        {
            foreach(GameObject ball in platformStack)
            {
                transform.position= Vector3.MoveTowards(transform.position, new Vector3(transform.position.x,
                                                                                        20f,
                                                                                        transform.position.z),
                                                                                        .5f * Time.deltaTime);

                ball.transform.position = Vector3.MoveTowards(ball.transform.position, new Vector3(transform.position.x,
                                                                                                   20f,
                                                                                                   transform.position.z),
                                                                                                   5f*Time.deltaTime);
            }
        }
    }

    public void PushBall(GameObject ball)
    {
        platformStack.Push(ball);
        ball.transform.position = new Vector3(transform.position.x, (platformStack.Count*0.5f)-0.25f ,transform.position.z);
    }
    public GameObject PopBall()
    {
        GameObject selectedBall = platformStack.Pop();
        selectedBall.transform.position = new Vector3(transform.position.x, 2.75f , transform.position.z);
        return selectedBall;
    }

    private void OnMouseDown()
    {
        if (!isOk)
        {
            if (TapSortManager.tSM.fromPlatform == null)
            {
                TapSortManager.tSM.fromPlatform = gameObject;
                TapSortManager.tSM.ArrangeSelections();
            }
            else if (TapSortManager.tSM.toPlatform == null)
            {
                if (platformStack.Count < 5)
                {
                    TapSortManager.tSM.toPlatform = gameObject;
                    TapSortManager.tSM.ArrangeSelections();
                }
            }
        }
    }
}
