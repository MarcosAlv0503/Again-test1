using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image bg;
    [SerializeField] private GameObject optMenu;

    public void startOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void optOnClick()
    {
        Color tc = HexToColor("#5E5E5E");
        tc.a = 1f;
        bg.CrossFadeColor(tc,1f, true, true);
        this.gameObject.SetActive(false);
        optMenu.SetActive(true);
    }
    public void exitOnClick()
    {
        Application.Quit();
    }


    Color HexToColor(string hex)
    {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}
