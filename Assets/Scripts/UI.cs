using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    private AsyncOperation asyncOperation;

    public GeneralController general;

    public float volume;
    public List<AudioSource> sounds;

    private bool reloadThis;
    private bool reload;

    public GameObject volumeOn;
    public GameObject volumeOff;
    public GameObject loadingScreen;
   // public Image loading;
    public GameObject settingScreen;
    public GameObject winScreen;
    public GameObject loseScreen;

    public float mode; // unique level
    public int howManyLevelsDone; // real number of last level
    private int levelMax; // how many levels total
    public float chosenLevel; // real number of level

    public int levelmoneyBonus;
    public TMP_Text levelmoneys;
    public int moneys;
    public int price1;
    public int price2;
    public int price3;
    public TMP_Text price1text;
    public TMP_Text price2text;
    public TMP_Text price3text;
    public List<TMP_Text> moneysText;

    // skills
    public float a2timer;
    public float a2timerMax;
    public Image a2activeskale;
    public Image a1button;
    public Image a2button;
    public Image a3button;
    public bool a2active;
    public bool a1active;
    public bool a3active;

    // tips
    public Animator tipAnimator;

    public int tutorial1;
    public int tutorial2;
    public int tutorial3;

    private int howmanyfragile;
    private int howmanystones;
    private int howmanyoff;
    private int howmanyrotate;
    private int howmanymove;
    public TMP_Text limitation;

    // public GameObject lightObject;
    //private bool lighton;

    public float timerforloading;
    public float timer;
    private float percents;
    public Image loadingScale;
    public TMP_Text loadingText;


    public void Start()
    {
        Time.timeScale = 1;
        asyncOperation = SceneManager.LoadSceneAsync("MainMenu");
        asyncOperation.allowSceneActivation = false;

        moneys = PlayerPrefs.GetInt("moneys");
        mode = PlayerPrefs.GetFloat("mode");
        levelMax = PlayerPrefs.GetInt("levelMax");
        volume = PlayerPrefs.GetFloat("volume");
        chosenLevel = PlayerPrefs.GetFloat("chosenLevel");
        howManyLevelsDone = PlayerPrefs.GetInt("howManyLevelsDone");

        tutorial1 = PlayerPrefs.GetInt("tutorial1");
        tutorial2 = PlayerPrefs.GetInt("tutorial2");
        tutorial3 = PlayerPrefs.GetInt("tutorial3");

        sounds[0].Play();
        if (volume == 1)
        {
            Sound(true);
        }
        else
        {
            Sound(false);
        }

        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        settingScreen.SetActive(false);
        loadingScreen.SetActive(false);

        tipAnimator.enabled = false;
        price1text.text = price1.ToString("0");
        price2text.text = price2.ToString("0");

        timer = timerforloading;


        // levels
            if (mode == 1 || mode == 0)
        {
            general.movementpossible = false;
            general.rotationpossible = false;
            general.destroyLimit = -1;
            howmanyfragile = 15;
            howmanystones = 8;
        }
        else if (mode == 2)
        {
            general.movementpossible = false;
            general.rotationpossible = true;
            general.destroyLimit = -1;
            howmanyfragile = 9;
            howmanystones = 7;
            howmanyrotate = 26;
            howmanymove = 28;
        }
        else if (mode == 3)
        {
            general.movementpossible = true;
            general.rotationpossible = true;
            general.destroyLimit = 30;
            howmanyfragile = 8;
            howmanystones = 6;
            howmanyrotate = 13;
            howmanymove = 20;
            howmanyoff = 10;
            general.leveltarget.transform.localPosition = new Vector2(1.5f, general.leveltarget.transform.localPosition.y);
            general.ballholder.transform.localPosition = new Vector2(0, general.ballholder.transform.localPosition.y);
        }
        else if (mode == 4) // fragile level
        {
            general.movementpossible = true;
            general.rotationpossible = true;
            general.destroyLimit = 30;
            howmanyfragile = 4;
            howmanystones = 8;
            howmanyrotate = 13;
            howmanymove = 14;
            howmanyoff = 16;
            general.ballholder.transform.localPosition = new Vector2(0, general.ballholder.transform.localPosition.y);
        }
        else if (mode == 5)
        {
            general.movementpossible = true;
            general.rotationpossible = true;
            general.destroyLimit = 25;
            howmanyfragile = 9;
            howmanystones = 2;
            howmanyrotate = 9;
            howmanymove = 15;
            howmanyoff = 30;
            general.leveltarget.transform.localPosition = new Vector2(2.1f, general.leveltarget.transform.localPosition.y);
            general.ballholder.transform.localPosition = new Vector2(-1.5f, general.ballholder.transform.localPosition.y);
        }
        else if (mode == 6) // stone level
        {
            general.movementpossible = true;
            general.rotationpossible = true;
            general.destroyLimit = 25;
            howmanyfragile = 9;
            howmanystones = 3;
            howmanyrotate =16;
            howmanymove = 9;
            general.leveltarget.transform.localPosition = new Vector2(1.5f, general.leveltarget.transform.localPosition.y);
            general.ballholder.transform.localPosition = new Vector2(-1.5f, general.ballholder.transform.localPosition.y);
        }
        else if (mode == 7) // rotating
        {
            general.movementpossible = true;
            general.rotationpossible = true;
            general.destroyLimit = 25;
            howmanyfragile = 9;
            howmanystones = 5;
            howmanyrotate = 1;
            howmanymove = 50;
            general.ballholder.transform.localPosition = new Vector2(0.5f, general.ballholder.transform.localPosition.y);
        }
        else if (mode == 8) // moving
        {
            general.movementpossible = true;
            general.rotationpossible = false;
            general.destroyLimit = 20;
            howmanyfragile = 8;
            howmanystones = 8;
            howmanyrotate = 0;
            howmanymove = 3;
            howmanyoff = 18;
            general.leveltarget.transform.localPosition = new Vector2(2.1f, general.leveltarget.transform.localPosition.y);
            general.ballholder.transform.localPosition = new Vector2(0.5f, general.ballholder.transform.localPosition.y);
        }
        else if (mode == 9)
        {
            general.movementpossible = true;
            general.rotationpossible = true;
            general.destroyLimit = 20;
            howmanyfragile = 7;
            howmanystones = 7;
            howmanyrotate = 10;
            howmanymove = 10;
            howmanyoff = 21;
            general.leveltarget.transform.localPosition = new Vector2(1.5f, general.leveltarget.transform.localPosition.y);
            general.ballholder.transform.localPosition = new Vector2(0f, general.ballholder.transform.localPosition.y);
        }
        else if (mode == 10)
        {
            general.movementpossible = false;
            general.rotationpossible = false;
            general.destroyLimit = 20;
            howmanyfragile = 6;
            howmanystones = 6;
            general.ballholder.transform.localPosition = new Vector2(0f, general.ballholder.transform.localPosition.y);
        }

        levelmoneyBonus = 100;

        foreach (GameObject obj in general.alldotsonfieldObj)
        {
            float randomRotation = Random.Range(0f, 360f);
            obj.transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);           
        }

        for (int i = 0; i < general.alldotsonfield.Count; i += 1) // normal = to break
        {
            general.alldotsonfield[i].sprite = general.possibleSprites[Random.Range(5, 10)];
            general.alldotsonfieldObj[i].tag = general.possibleTags[1];
            general.alldotsonfield[i].color = Color.white;
        }

        if (howmanyoff > 0) // off
        {
            for (int i = 0; i < general.alldotsonfieldObj.Count; i += howmanyoff)
            {
                general.alldotsonfieldObj[i].SetActive(false);
            }
        }

        for (int i = 0; i < general.alldotsonfield.Count; i += howmanyfragile) // every fragile
        {
            general.alldotsonfield[i].sprite = general.possibleSprites[Random.Range(10, 15)];
            general.alldotsonfieldObj[i].tag = general.possibleTags[2];
            general.alldotsonfield[i].color = general.transp;
        }
        for (int i = 0; i < general.alldotsonfield.Count; i += howmanystones) // stone
        {
            general.alldotsonfield[i].sprite = general.possibleSprites[Random.Range(0, 5)];
            general.alldotsonfieldObj[i].tag = general.possibleTags[0];
            general.alldotsonfield[i].color = general.grey;
        }
        if (general.rotationpossible)
        {
            for (int i = 0; i < general.allDotScripts.Count; i += howmanyrotate)
            {
                general.allDotScripts[i].rotateornnot = true;
            }
        }
        if (general.movementpossible)
        {
            for (int i = 0; i < general.allDotScripts.Count; i += howmanymove)
            {
                general.allDotScripts[i].moveornot = true;
            }
        }

        foreach (DotFieldController script in general.allDotScripts)
        {
            PolygonCollider2D m_Collider = script.gameObject.AddComponent<PolygonCollider2D>();

            // Vector3 spriteHalfSize = script.rend.sprite.bounds.extents;
            // script.thiscollider.radius = spriteHalfSize.x > spriteHalfSize.y ? spriteHalfSize.x : spriteHalfSize.y;
        }

        if (general.destroyLimit != -1)
        {
            limitation.text = "touches left: " + general.destroyLimit.ToString("0");
        }
        else
        {
            limitation.text = "";
        }
        levelmoneys.text = "+" + levelmoneyBonus.ToString("0") + "!";

        if (tutorial1 == 0)
        {
            foreach(GameObject a in general.skills)
            {
                a.SetActive(false);
            }
            tipAnimator.enabled = false;
            tipAnimator.Play("Start");
            tipAnimator.enabled = true;
        }
        else if (tutorial2 != 0 && tutorial3 == 0)
        {
            tipAnimator.enabled = false;
            tipAnimator.Play("Bonuses");
            tipAnimator.enabled = true;
        }

        general.balltolaunch.isKinematic = true;
    }
    public void win()
    {
        if (!winScreen.activeSelf)
        {
            sounds[2].Play();
            general.paused = true;
            Debug.Log("win");
            winScreen.SetActive(true);
            if (chosenLevel > howManyLevelsDone)
            {
                PlayerPrefs.SetInt("howManyLevelsDone", (int)chosenLevel);
            }
            moneys += levelmoneyBonus;
            PlayerPrefs.SetInt("moneys", moneys);
            PlayerPrefs.Save();
        }

    }
    public void lose()
    {

        
        general.paused = true;
        loseScreen.SetActive(true);

    }

    public void launchBall()
    {
        if (!general.paused && !general.ballLaunched)
        {
            sounds[1].Play();
            general.ballLaunched = true;
            general.balltolaunch.isKinematic = false;
            //general.balltolaunch.AddForce(Vector2.up*-1, ForceMode2D.Impulse);

            if (tutorial1 != 0 && tutorial2 == 0)
            {
                tutorial2 = 1;
                PlayerPrefs.SetInt("tutorial2", 1);
                PlayerPrefs.Save();
                tipAnimator.enabled = false;
                tipAnimator.gameObject.SetActive(false);
            }
        }
    }

    public void Update()
    {
        //if (!lighton)
        //{
        //    lighton = true;
        //    GameObject lightObj = Instantiate(lightObject, transform.position, Quaternion.Euler(60, -30, 0));
        //    lightObj.transform.position = Vector3.zero;
        //}
        
        foreach (TMP_Text text in moneysText)
        {
            text.text = moneys.ToString("0");
        }


        if (loadingScreen.activeSelf == true)
        {
            foreach (AudioSource audio in sounds)
            {
                audio.volume = 0;
            }
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                loadingScale.fillAmount = 1 - (timer / timerforloading);
                percents = loadingScale.fillAmount * 100;
                loadingText.text = percents.ToString("0") + "%";
            }
            else
            {
                if (!reload)
                {
                    reload = true;
                    if (reloadThis)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                    else
                    {
                        asyncOperation.allowSceneActivation = true;
                    }
                }
           }
        }
        if (!loadingScreen.activeSelf)
        {
            foreach (AudioSource audio in sounds)
            {
                audio.volume = volume;
            }
        }

        tipAnimator.SetFloat("bonusesHere", tutorial2);
    }

    public void ExitMenu()
    {
        sounds[1].Play();
        general.paused = false;
        timer = timerforloading;
        loadingScreen.SetActive(true);
        //loading.fillAmount = 0;
        //loading.enabled = false;
    }
    public void reloadScene()
    {
        sounds[1].Play();
        //general.paused = false;
        //loading.fillAmount = 0;
        reloadThis = true;
        loadingScreen.SetActive(true);
    }
    public void Sound(bool volumeBool)
    {
        if (volumeBool)
        {
            volumeOn.SetActive(true);
            volumeOff.SetActive(false);
            volume = 1;
        }
        else
        {
            volume = 0;
            volumeOn.SetActive(false);
            volumeOff.SetActive(true);
        }

        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    public void closeIt()
    {
        foreach (DotFieldController script in general.allDotScripts)
        {
            script.enabled = true;
        }
        sounds[1].Play();
        general.paused = false;
        settingScreen.SetActive(false);
    }

    public void Settings()
    {
        foreach (DotFieldController script in general.allDotScripts)
        {
            script.enabled = false;
        }
        sounds[1].Play();
        general.paused = true;
        settingScreen.SetActive(true);
    }

    public void a1()
    {
        sounds[1].Play();

        if (moneys >= price1)
        {
            if (!a1active)
            {
                a1button.color = new Color32(255, 255, 255, 130);
                a1active = true;
                moneys -= price1;
                PlayerPrefs.SetInt("moneys", moneys);
                PlayerPrefs.Save();
            }
        }
        else
        {
           //tipAnimator.enabled = false;
            tipAnimator.Play("Warning");
            tipAnimator.enabled = true;

        }
    }

    public void a2()
    {
        sounds[1].Play();

        if (moneys >= price2)
        {
            if (!a2active)
            {
                moneys -= price2;
                PlayerPrefs.SetInt("moneys", moneys);
                PlayerPrefs.Save();
                a2active = true;
                a2button.color = new Color32(255, 255, 255, 130);
                a2timer = a2timerMax;
            }
        }
        else
        {

             //tipAnimator.enabled = false;
            tipAnimator.Play("Warning");
            tipAnimator.enabled = true;

        }
    }
    public void a3()
    {
        sounds[1].Play();

        if (moneys >= price3)
        {
            if (!a3active)
            {
                a3button.color = new Color32(255, 255, 255, 130);
                a3active = true;
                moneys -= price3;
                PlayerPrefs.SetInt("moneys", moneys);
                PlayerPrefs.Save();
            }
        }
        else
        {
            //tipAnimator.enabled = false;
            tipAnimator.Play("Warning");
            tipAnimator.enabled = true;

        }
    }
    public void NextLevel()
    {
        sounds[1].Play();
        if (chosenLevel <= howManyLevelsDone + 1 && chosenLevel != levelMax)
        {
            chosenLevel += 1;
            mode += 1;
            if (mode > 10)
            {
                mode = 1;
            }


            PlayerPrefs.SetFloat("chosenLevel", chosenLevel);
            PlayerPrefs.SetFloat("mode", mode);
            PlayerPrefs.Save();
            reloadScene();
        }
    }
}
