using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gM = null;

    void Awake()
    {
        if (gM == null)
        {
            gM = this;
            DontDestroyOnLoad(this);
        }
        else if (this != gM)
        {
            Destroy(gameObject);
        }
    }


    public GameObject ballPrefab;
    GameObject ball;

    GameObject spawnPoint;

    int points;
    public Text pointsText;

    private void Start()
    {
        points = 0;
        pointsText.text = "Points="+points;
        spawnPoint = GameObject.Find("Spawn Point");
        ball = Instantiate(ballPrefab,spawnPoint.transform.position,Quaternion.identity);
    }

    public void Reset(int point)
    {
        points += point;
        pointsText.text = "Points="+points;

        Destroy(GameObject.FindGameObjectWithTag("Player"));
        ball = Instantiate(ballPrefab, spawnPoint.transform.position, Quaternion.identity);

    }


}
