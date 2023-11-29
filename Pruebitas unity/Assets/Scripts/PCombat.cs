using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCombat : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private Transform attPoint;
    [SerializeField] private float attRange = 10f;


    public float attForce = 1f;
    public float attRate = 2f;
    private float nextAttTime = 0f;

    public float damage = 25f;

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttTime)
        {
            if (Input.GetButtonDown("Attack"))
            {
                anim.SetTrigger("Attack");
                nextAttTime = Time.time + 1f / attRate;
            }
        }
        
    }

    private void Attack()
    {
        
        Collider2D[] hittedEnemys = Physics2D.OverlapCircleAll(attPoint.position, attRange, enemyLayers);

        foreach (Collider2D enemy in hittedEnemys)
        {
            if (enemy.GetComponent<Enemy>().vulnerable)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
                //Vector2 vec = new Vector2(enemy.transform.position.x - transform.position.x, enemy.transform.position.y - transform.position.y);
                Vector2 vec = enemy.transform.position - transform.position;
                vec.y = vec.y + 0.5f;
                Vector2 uvec = vec / vec.magnitude;
                Vector2 knockback = (uvec * attForce) / enemy.GetComponent<Enemy>().weight;
                print(knockback);
                //vec.normalized?
                enemy.GetComponent<Rigidbody2D>().velocity = knockback;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attPoint == null || attRange == 0)
        {
            return;
        }
        Gizmos.DrawWireSphere(attPoint.position, attRange);
    }
}
