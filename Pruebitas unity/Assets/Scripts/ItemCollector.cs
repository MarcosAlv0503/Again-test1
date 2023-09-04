using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Text txtBox;
    [SerializeField] private AudioSource collectSound;

    private int cal = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pumpkin"))
        {
            cal++;
            Destroy(collision.gameObject);
            collectSound.Play();
            Notificate("+1 Calabaza (" + cal + ")");
        }
    }

    private void Notificate(string txt)
    {
        txtBox.text = txt;
        txtBox.CrossFadeAlpha(1f,0f,false);
        txtBox.CrossFadeAlpha(0f, 1.4f, false);
    }
}
