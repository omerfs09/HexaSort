using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainSceneLoader : MonoBehaviour
{

    public TextMeshProUGUI loadingTMP;
    float timer = 0;
    float timer2 = 0;
    void Start()
    {
                
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        if(timer < 0.2f)
        {
            loadingTMP.text = "Loading";
        }
        else if(timer < 0.4f)
        {
            loadingTMP.text = "Loading.";

        }
        else if (timer < 0.6f)
        {
            loadingTMP.text = "Loading..";

        }
        else if (timer < 0.8f)
        {
            loadingTMP.text = "Loading...";

        }
        else if(timer > 1f)
        {
            timer = 0;
        }
        if(timer2 > 3)
        {
            SceneManager.LoadScene("SampleScene");
            timer2 = 0;
        }

    }

}
