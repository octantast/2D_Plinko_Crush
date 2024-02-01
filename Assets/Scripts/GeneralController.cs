using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GeneralController : MonoBehaviour
{
    public UI ui;

    // dots
    public List<SpriteRenderer> alldotsonfield;
    public List<GameObject> alldotsonfieldObj;
    public List<DotFieldController> allDotScripts;
    public List<Sprite> possibleSprites;
    public List<string> possibleTags;
    public List<GameObject> skills;

    public Color32 grey;
    public Color32 transp;


    public GameObject leveltarget;
    public Rigidbody2D balltolaunch;
    public GameObject ballholder;

    public bool paused;
    public bool ballLaunched;

    public bool movementpossible;
    public bool rotationpossible;
    public float destroyLimit;

    public GameObject tocheck;

    // effects

    public List<ParticleSystem> glass;
    //public ParticleSystem hole;
    //public ParticleSystem iceParticles;

    public void ballDone()
    {
        ui.win();
    }
    public void Update()
    {
        if (!paused)
        {

            if (ui.a2timer > 0)
            {
                ui.a2timer -= Time.deltaTime;
                ui.a2activeskale.fillAmount = 1 - ui.a2timer / ui.a2timerMax;
            }
            else
            {
                ui.a2button.color = new Color32(255, 255, 255, 255);
                ui.a2activeskale.fillAmount = 0;
                ui.a2active = false;
            }
        }
    }

    public void DestroyObj(GameObject obj, string tagObj, DotFieldController script)
    {
        tocheck = obj;
        if (!ballLaunched)
        {
            if (destroyLimit > 0 && !ui.a2active)
            {
                destroyLimit -= 1;
                ui.limitation.text = "touches left: " + destroyLimit.ToString("0");

            }

            if (tagObj == "Fragile")
            {
                Instantiate(glass[Random.Range(0, glass.Count)], tocheck.transform.position, Quaternion.identity);
                allDotScripts.Remove(script);
                ui.sounds[3].Play();
                Destroy(tocheck);
                if (!ui.a1active && !ui.a2active)
                {
                    ui.lose();
                }
            }
            else if (tagObj == "Broking")
            {
                uicheck();
                Instantiate(glass[Random.Range(0, glass.Count)], tocheck.transform.position, Quaternion.identity);
                ui.sounds[4].Play();
                allDotScripts.Remove(script);
                Destroy(tocheck);
            }

            if (destroyLimit == 0)
            {
                ui.lose();
            }
        }
    }

    public void uicheck()
    {
        if (ui.tutorial1 == 0)
        {
            ui.tutorial1 = 1;
            PlayerPrefs.SetInt("tutorial1", 1);
            PlayerPrefs.Save();
            ui.tipAnimator.Play("Start2");
            ui.tipAnimator.enabled = true;
        }

    }
}
