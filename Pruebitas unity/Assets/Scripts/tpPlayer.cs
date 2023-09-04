using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("tp"))
        {
            transform.position = new Vector2(58, -6);
        }
    }
}
