using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class NPCDialog : MonoBehaviour
{
    [SerializeField] private string[] dialog;
    [SerializeField] private GameObject key;
    [SerializeField] private TextMeshProUGUI txtBox;
    [SerializeField] private float txtSpeed;

    private int index;
    private bool near = false;
    private bool inDialog = false;

    private void Update()
    {
        if (Input.GetButtonDown("Action") && near && !inDialog)
        {
            txtBox.text = string.Empty;
            startDialog();
        }
        if (Input.GetButtonDown("Jump") && inDialog)
        {
            if(txtBox.text == dialog[index])
            {
                nextLine();
            }
            else
            {
                StopAllCoroutines();
                txtBox.text = dialog[index];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        key.SetActive(true);
        near = true;
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        key.SetActive(false);
        near = false;
    }

    private void startDialog()
    {
        inDialog = true;
        index = 0;
        SceneController.instance.paused = true;
        Time.timeScale = 0f;
        txtBox.GetComponentInParent<Image>().enabled = true;
        txtBox.enabled = true;
        AudioListener.pause = true;
        StartCoroutine(TypeLine());
        
    }
    
    IEnumerator TypeLine()
    {
        foreach (char c in dialog[index].ToCharArray())
        {
            txtBox.text += c;
            yield return new WaitForSecondsRealtime(txtSpeed);
        }
    }
    void nextLine()
    {
        if(index < dialog.Length - 1)
        {
            index++;
            txtBox.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            txtBox.enabled = false;
            txtBox.GetComponentInParent<Image>().enabled = false;
            inDialog = false;
            SceneController.instance.paused = false;
            Time.timeScale = 1f;
            AudioListener.pause = false;
        }
    }
}
