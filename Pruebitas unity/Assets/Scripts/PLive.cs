using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PLive : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private Image heart;
    //[SerializeField] private Canvas canvas;
    private Image[] heartArray;

    [SerializeField] private AudioSource deathSound;

    private int maxHealth, health;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        maxHealth = 3;
        health = maxHealth;
        heartArray = new Image[maxHealth];

        for(int i = 0; i < maxHealth; i++)
        {
            if(i == 0)
            {
                heartArray[i] = heart;
            }
            else
            {
                Transform tr = heart.transform.parent;
                heartArray[i] = Instantiate(heart, tr);
                heartArray[i].transform.position = new Vector2(heartArray[i].transform.position.x + 50*i, heartArray[i].transform.position.y);
            }
            //heartArray[i].transform.parent = canvas.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Hurt();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Hurt();
        }
    }

    private void Hurt()
    {
        health--;
        heartArray[health].enabled = false;
        anim.SetTrigger("Hurt");
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Death");
        deathSound.Play();
    }

    private void RestartLevel()
    {
        Debug.Log("entro xd");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}   
