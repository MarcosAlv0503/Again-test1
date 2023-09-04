using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private Vector2 playerMapPos;
    private bool hasEnter = false;

    public static SceneController instance;

    public Vector2 GetplayerMapPos()
    {
        return playerMapPos;
    }

    public void SetplayerMapPos(Vector2 pos)
    {
        this.playerMapPos = pos;
    }

    public bool GetHasEnter()
    {
        return hasEnter;
    }

    public void SetHasEnter(bool enter)
    {
        this.hasEnter = enter;
    }

    private void Awake()
    {
        Debug.Log("cargo correctamente");
        if(SceneController.instance == null){
            SceneController.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
