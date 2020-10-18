using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapSortManager : MonoBehaviour
{
    public static TapSortManager tSM = null;

    void Awake()
    {
        if (tSM == null)
        {
            tSM = this;
            DontDestroyOnLoad(this);
        }
        else if (this != tSM)
        {
            Destroy(gameObject);
        }
    }

    [Header("Prefabs")]
    public GameObject redBallPrefab;
    public GameObject blueBallPrefab;
    public GameObject yellowBallPrefab;

    [Header("Arrays & Lists")]
    public GameObject[] platforms;
    private List<GameObject> balls=new List<GameObject>();

    [Header("GameObjects")]
    public GameObject fromPlatform=null;
    public GameObject toPlatform=null;
    public GameObject selectedBall=null;

    private bool firstMovBool;
    private bool secondMovBool;
    private int finishedPlatformsCounter;

    private void Start()
    {
        firstMovBool = false;
        secondMovBool = false;
        finishedPlatformsCounter = 0;

        for(int i = 0; i < 5; i++)
        {
            balls.Add(Instantiate(redBallPrefab));
        }
        for (int i = 0; i < 5; i++)
        {
            balls.Add(Instantiate(blueBallPrefab));
        }
        for (int i = 0; i < 5; i++)
        {
            balls.Add(Instantiate(yellowBallPrefab));
        }

        DistributeBalls();
    }
    private void Update()
    {
        if (firstMovBool)
        {
            selectedBall.transform.position = Vector3.MoveTowards(selectedBall.transform.position, new Vector3(toPlatform.transform.position.x,
                                                                                                                2.75f, toPlatform.transform.position.z), 
                                                                                                                5f * Time.deltaTime);
        }
        if (secondMovBool)
        {
            selectedBall.transform.position = Vector3.MoveTowards(selectedBall.transform.position, new Vector3(toPlatform.transform.position.x, 
                                                                                                              (toPlatform.GetComponent<PlatformStack>().platformStack.Count * 0.5f) - 0.25f, 
                                                                                                               toPlatform.transform.position.z), 
                                                                                                               2f * Time.deltaTime);
        }
    }

    private void DistributeBalls()
    {
        for (int i = 0; i < 15; i++)
        {
            int a = Random.Range(0, 5);
            int b = Random.Range(0, balls.Count);

            while (platforms[a].GetComponent<PlatformStack>().platformStack.Count > 4)
            {
                a = Random.Range(0, 5);
            }

            platforms[a].GetComponent<PlatformStack>().PushBall(balls[b]);
            balls.RemoveAt(b);
        }
    }

    public void ArrangeSelections()
    {
        if (toPlatform == null)
        {
            selectedBall = fromPlatform.GetComponent<PlatformStack>().PopBall();
        }
        else
        {
            StartCoroutine(SendBallToTarget(selectedBall, toPlatform));
        }
    }

    private IEnumerator SendBallToTarget(GameObject selectedBall, GameObject to)
    {        
        while(selectedBall.transform.position != new Vector3(to.transform.position.x,
                                                             2.75f, 
                                                             to.transform.position.z))
        {
            firstMovBool = true;
            yield return null;
        }

        firstMovBool = false;
        toPlatform.GetComponent<PlatformStack>().PushBall(selectedBall);

        while(selectedBall.transform.position!= new Vector3(to.transform.position.x, 
                                                           (to.GetComponent<PlatformStack>().platformStack.Count * 0.5f) - 0.25f, 
                                                            to.transform.position.z))
        {
            secondMovBool = true;
            yield return null;
        }

        if (to.GetComponent<PlatformStack>().platformStack.Count == 5)
        {
            CheckOrder();
        }

        secondMovBool = false;
        selectedBall = null;
        fromPlatform = null;
        toPlatform = null;
    }

    private void CheckOrder()
    {

        List<GameObject> tempList = new List<GameObject>();
        bool controlBool= false;
        for(int i = 0; i < 5; i++)
        {
            tempList.Add(toPlatform.GetComponent<PlatformStack>().platformStack.Pop());
        }
        for(int i = 0; i < 4; i++)
        {
            if (tempList[0].tag == tempList[i].tag)
            {
                controlBool = true;
            }
            else
            {
                controlBool = false;
                break;
            }
        }

        for(int i = 0; i < 5; i++)
        {
            toPlatform.GetComponent<PlatformStack>().platformStack.Push(tempList[tempList.Count-1]);
            tempList.RemoveAt(tempList.Count - 1);
        }

        if (controlBool == true)
        {
            toPlatform.GetComponent<PlatformStack>().isOk = true;

            finishedPlatformsCounter++;

            if (finishedPlatformsCounter == 3)
                StartCoroutine(ResetScene());
        }
        tempList.Clear();
    }

    private IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(3);
        Destroy(tSM);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
