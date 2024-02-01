using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotFieldController : MonoBehaviour
{
    public PolygonCollider2D thiscollider;
    public GeneralController general;
    public bool rotateornnot;
    public bool moveornot;
    public float speed;
    private Vector2 localPos;

    private bool isMoving;
    public SpriteRenderer rend;

    public Vector2 touchPos;
    public Vector2 touchPosWorld;

    private bool colliders;

    public void Start()
    {
        rend = GetComponent<SpriteRenderer>();
       
           localPos = transform.localPosition;
    }
    void Update()
    {
        if (!colliders)
        {
            thiscollider = GetComponent<PolygonCollider2D>();
            colliders = true;
            
        }

        if (!general.paused)
        {
            if (rotateornnot && !isMoving)
        {
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0f, 0f, 400f), Time.deltaTime * speed);
        }
        if(moveornot && !isMoving)
        {
            float lerpValue = Mathf.PingPong(Time.time * 0.5f, 1);
            float targetValue = Mathf.Lerp(localPos.y-0.1f, localPos.y + 0.1f, lerpValue);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, targetValue, transform.localPosition.z), speed * Time.deltaTime);
        }

            if (Application.isEditor)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    touchPos = Input.mousePosition;
                    touchPosWorld = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, Camera.main.nearClipPlane));
                    RaycastHit2D hit = Physics2D.Raycast(touchPosWorld, new Vector3(0, 0, Camera.main.nearClipPlane));
                    if (hit.collider != null && hit.collider == thiscollider)
                    {
                        Debug.Log("touchPosWorld");

                        if (!general.ui.a3active || gameObject.tag != "Stone")
                        {
                            general.DestroyObj(transform.gameObject, gameObject.tag, this);
                        }
                        else if (gameObject.tag == "Stone")
                        {
                            isMoving = true;
                        }

                    }
                }
                if (Input.GetMouseButton(0))
                {
                    touchPos = Input.mousePosition;
                    touchPosWorld = Camera.main.ScreenToWorldPoint(touchPos);
                    continueTouch();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    endTouch();
                }

            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    touchPos = touch.position;
                    touchPosWorld = Camera.main.ScreenToWorldPoint(touchPos);

                    if (thiscollider.OverlapPoint(touchPosWorld))
                    {
                        Debug.Log("hit");
                        if (!general.ui.a3active || gameObject.tag != "Stone")
                        {
                            general.DestroyObj(transform.gameObject, gameObject.tag, this);
                        }
                        else if (gameObject.tag == "Stone")
                        {
                            isMoving = true;
                        }
                    }
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    touchPos = touch.position;
                    touchPosWorld = Camera.main.ScreenToWorldPoint(touchPos);
                    continueTouch();
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    endTouch();
                }
            }
        }
    }


    public void continueTouch()
    {
        if (!general.paused && isMoving)
        {
            if (isMoving)
            {
                transform.position = new Vector3(touchPosWorld.x, touchPosWorld.y, 0);

            }
        }
    }

    public void endTouch()
    {
        if (!general.paused)
        {
            if (isMoving)
            {
                isMoving = false;
                general.ui.a3active = false;
                general.ui.a3button.color = Color.white;
            }
        }
    }
}
