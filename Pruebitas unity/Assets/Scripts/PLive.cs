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
    private Image[] heartArray;
    private bool vulnerable = true;

    [SerializeField] private AudioSource deathSound;
    public int health;
    public int maxHealth = 3;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        health = maxHealth;
        heartArray = new Image[health];

        for (int i = 0; i < health; i++)
        {
            if (i == 0)
            {
                heartArray[i] = heart;
            }
            else
            {
                Transform tr = heart.transform.parent;
                heartArray[i] = Instantiate(heart, tr);
                heartArray[i].transform.position = new Vector2(heartArray[i].transform.position.x + 50 * i, heartArray[i].transform.position.y);
            }
        }
    }
    private void Update()
    {
        
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

    public void Hurt()
    {
        if (vulnerable)
        {
            vulnerable = false;
            health--;
            anim.SetTrigger("Hurt");
            heartArray[health].enabled = false;
            if (health <= 0)
            {
                Die();
            }
        }
        StartCoroutine(Cooldown(1f));
    }

    IEnumerator Cooldown(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        vulnerable = true;
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Death");
        deathSound.Play();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}   
