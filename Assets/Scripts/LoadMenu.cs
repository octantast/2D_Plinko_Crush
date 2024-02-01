using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadMenu : MonoBehaviour
{
    public Image loadingScale;
    public TMP_Text loadingText;
    public float timerforloading;
    private float timer;
    private float percents;

    private void Start()
    {
        timer = timerforloading;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            loadingScale.fillAmount = 1 - (timer / timerforloading);
            percents = loadingScale.fillAmount * 100;
            loadingText.text = percents.ToString("0") + "%";
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
