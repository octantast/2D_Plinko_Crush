using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GeneralController general;
    public UI ui;
    public Rigidbody2D rb;
    public Collider2D collide;

    private Collider2D thatCollider;
    public AudioSource sound;

    public bool dot;
    private bool portal;
    private void Start()
    {
        collide = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if(general.paused)
        {
            rb.velocity = Vector2.zero;
            if(portal && transform.localScale.x > 0)
            {
                transform.localScale -= new Vector3(0.001f, 0.001f, 0);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Broking") || collision.gameObject.CompareTag("Fragile") || collision.gameObject.CompareTag("Stone"))
        {
            sound.Play();

        }
           

        if(collision.gameObject.CompareTag("Bottom"))
        {
            ui.lose();
        }
        if (collision.gameObject.CompareTag("Portal"))
        {
            portal = true;
        }
    }

    }
