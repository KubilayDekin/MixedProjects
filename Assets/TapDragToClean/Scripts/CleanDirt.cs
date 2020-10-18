using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CleanDirt : MonoBehaviour
{
    public Texture2D deformableTexture;
    public Texture2D cloneTexture;
    public Text informationText;

    //496300-495900 // 495500 yapalım.
    //408500 en başta
    private void Start()
    {
        cloneTexture = Instantiate(deformableTexture);
        gameObject.GetComponent<Renderer>().material.mainTexture = cloneTexture;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(EstimateTransparentPix(cloneTexture));
        }
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Vector2 hittedPixels = hit.textureCoord;
                hittedPixels.x *= 800;
                hittedPixels.y *= 800;

                for (int i = 0; i < 25; i++)
                {
                    for (int j = 0; j < 25; j++)
                    {
                        cloneTexture.SetPixel((int)(hittedPixels.x - i), (int)(hittedPixels.y - j), new Color(0, 0, 0, 0));
                        cloneTexture.SetPixel((int)(hittedPixels.x + i), (int)(hittedPixels.y - i), new Color(0, 0, 0, 0));
                        cloneTexture.SetPixel((int)(hittedPixels.x - i), (int)(hittedPixels.y + j), new Color(0, 0, 0, 0));
                        cloneTexture.SetPixel((int)(hittedPixels.x + i), (int)(hittedPixels.y + j), new Color(0, 0, 0, 0));
                    }
                }

                cloneTexture.Apply();
            }
        }        
    }
    public void CheckWin()
    {
        if (EstimateTransparentPix(cloneTexture) > 495000)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            informationText.text = "Hala silinmemiş lekeler var !";
        }
    }
    private int EstimateTransparentPix(Texture2D t)
    {
        int SkipPixels = 5;
        int tested = 0;
        int tran = 0;
        int x = 0;
        while (x < t.width)
        {
            int y = 0;
            while (y < t.height)
            {
                tested++;
                if (t.GetPixel(x, y).a < .5f) { tran++; }
                y += SkipPixels;
            }
            x += SkipPixels;
        }
        float percent = (float)tran / (float)tested;
        tran = Mathf.RoundToInt(t.width * t.height * percent);
        return tran;
    }
}
