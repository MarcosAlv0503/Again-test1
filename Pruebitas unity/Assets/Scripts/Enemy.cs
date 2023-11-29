using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public bool hostile = false;
    public bool vulnerable = true;
    public float weight = 10;
    public float maxHealth = 100;
    public bool attack = false;
    public bool flipFollowPlayer;


    public bool isFlipped = false;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        flipFollowPlayer = true;
    }
    private void Update()
    {
        if (flipFollowPlayer)
        {
            LookAtPlayer();
        }
    }

    public void TakeDamage(float damage)
    {
        if (vulnerable)
        {
            vulnerable = false;
            currentHealth -= damage;
            print(currentHealth);

            if (currentHealth <= 0)
            {
                anim.SetTrigger("Death");
            }
            else
            {
                anim.SetTrigger("Hurt");
            }
            StartCoroutine(Cooldown());
        }
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        vulnerable = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (attack)
        {
            if (collision.gameObject.CompareTag("Player")) {
                collision.gameObject.GetComponent<PLive>().Hurt();
            }
        }
    }

    private void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }

    private void LookAtPlayer()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        if (transform.position.x > player.position.x)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
            isFlipped = true;
        }
        else if (transform.position.x < player.position.x)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            isFlipped = false;
        }
    }
}
