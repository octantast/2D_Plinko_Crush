using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour//, IBeginDragHandler, IDragHandler, IEndDragHandler, 
{
    public Image thisImage;
    public float startSize;
    public Color32 startColor;

    void Start()
    {
        thisImage = transform.GetComponent<Image>();
        startSize = transform.localScale.x;
        startColor = thisImage.color;
    }
    private void FixedUpdate()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(startSize, startSize, startSize), 10 * Time.deltaTime);
    }
    public void OnDown()
    {
        transform.localScale = new Vector3(startSize + 0.1f, startSize + 0.1f, startSize + 0.1f);
    }

  
}
