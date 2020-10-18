using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TDTS_Manager : MonoBehaviour
{
    public static TDTS_Manager tDTS = null;

    void Awake()
    {
        if (tDTS == null)
        {
            tDTS = this;
            DontDestroyOnLoad(this);
        }
        else if (this != tDTS)
        {
            Destroy(gameObject);
        }
    }

    [Header("Prefabs")]
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    public GameObject capsulePrefab;

    [Header("Lists And Arrays")]
    public List<GameObject> objects;
    public GameObject[] sections;
    public GameObject[] subsections;

    [HideInInspector]
    public GameObject fromSection;
    [HideInInspector]
    public GameObject toSection;

    [Header("Materials")]
    public Material highlightedMat;
    public Material defaultMat;

    [Header("Booleans")]
    public bool isDragging;
    private int completedSectionCounter;

    private void Start()
    {
        completedSectionCounter = 0;
        SendParameters();
    }
    private void Update()
    {
        if (isDragging)
            HighlightSubsection();
    }
    private void SendParameters()
    {
        InstantiateObject(cubePrefab);
        InstantiateObject(spherePrefab);
        InstantiateObject(capsulePrefab);
    }
    private void InstantiateObject(GameObject prefab)
    {
        for(int i = 0; i < 4; i++)
        {
            int a = Random.Range(0, 16);

            while (subsections[a].GetComponent<Subsection>().isEmpty == false)
            {
                a = Random.Range(0, 16);
            }

            subsections[a].GetComponent<Subsection>().objectOnSection = Instantiate(prefab,new Vector3(subsections[a].transform.position.x,prefab.transform.position.y, subsections[a].transform.position.z),Quaternion.identity);
            subsections[a].GetComponent<Subsection>().objectOnSection.transform.parent = subsections[a].transform;
            subsections[a].GetComponent<Subsection>().isEmpty = false;
        }
    }
    private void HighlightSubsection()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            hit.collider.gameObject.GetComponent<Renderer>().material = highlightedMat;
        }
    }
    public void CheckWin()
    {
        completedSectionCounter++;
        if (completedSectionCounter == 3)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
