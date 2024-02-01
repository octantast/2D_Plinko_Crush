using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkingScript : MonoBehaviour
{
    public SpriteRenderer thissprite;
    public float blinkSpeed = 1.0f;
    private Color32 thiscolor;
    public Color32 targetcolor;
    //private TMP_Text text;
    //private Color32 whitecolor = new Color32(225, 255, 255, 60);

    private void Start()
    {
        // text = GetComponent<TMP_Text>();
        // thiscolor = text.color;
        thiscolor = thissprite.material.color;
    }

    void Update()
    {
        thissprite.material.color = Color.Lerp(thiscolor, targetcolor, Mathf.PingPong(Time.time, 2f) / 2f);
        //text.color = Color.Lerp(thiscolor, whitecolor, Mathf.PingPong(Time.time, 2f) / 2f);
    }
}
