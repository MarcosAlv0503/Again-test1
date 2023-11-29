using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptMenu : MonoBehaviour
{
    [SerializeField] private Image bg;
    [SerializeField] private GameObject mainMenu;

    public AudioMixer audioMixer;

    public void backOnClick()
    {
        Color tc = HexToColor("#B9B9B9");
        tc.a = 1f;
        bg.CrossFadeColor(tc, 1f, true, true);
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }

    Color HexToColor(string hex)
    {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

    public void setVolume(float volume)
    {
        if(volume <= -39)
        {
            volume = -80;
        }
        audioMixer.SetFloat("volume", volume);
    }
}
