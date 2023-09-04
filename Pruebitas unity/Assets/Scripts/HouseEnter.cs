using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseEnter : MonoBehaviour
{
    private AudioSource doorSound;
    [SerializeField] private string scene;
    [SerializeField] private bool exit;
    void Start()
    {
        doorSound = GetComponent<AudioSource>();
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneController.instance.playerLastPos = collision.gameObject.transform;
            doorSound.Play();
            ChangeLevel();
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (SceneController.instance.GetHasEnter() == false)
            {
                if (exit == false)
                {
                    SceneController.instance.SetplayerMapPos(collision.gameObject.transform.position);
                    doorSound.Play();
                    SceneController.instance.SetHasEnter(true);
                    ChangeLevel();
                }
            }
            if (exit)
            {
                PMovement.position = true;
                doorSound.Play();
                ChangeLevel();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (exit == false)
            {
                SceneController.instance.SetHasEnter(false);
            }
        }
    }

    private void ChangeLevel()
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
