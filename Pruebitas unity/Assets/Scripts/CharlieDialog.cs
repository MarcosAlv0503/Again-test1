using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CharlieDialog : MonoBehaviour
{
    [SerializeField] private string[] dialog;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private GameObject btn;
    [SerializeField] private float txtSpeed;

    private int index;
    private bool skippeable = true;
    private bool near = false;
    private bool inDialog = false;
    private TextMeshProUGUI[] txtBox;
    private GameObject[] buttons;

    //0 = normal, 1 = question
    private int usedTxtBox = 0;

    private void Start()
    {
        txtBox = dialogBox.GetComponentsInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {

        if (Input.GetButtonDown("Action") && near && !inDialog)
        {
            txtBox[0].text = string.Empty;
            txtBox[1].text = string.Empty;
            print(dialog);
            startDialog();
        }
        if (Input.GetButtonDown("Jump") && inDialog)
        {
            if (txtBox[usedTxtBox].text == dialog[index] && skippeable)
            {
                nextLine();
            }
            else
            {
                StopAllCoroutines();
                txtBox[usedTxtBox].text = dialog[index];
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
        dialogBox.SetActive(true);
        AudioListener.pause = true;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in dialog[index].ToCharArray())
        {
            txtBox[usedTxtBox].text += c;
            yield return new WaitForSecondsRealtime(txtSpeed);
        }
    }
    void nextLine()
    {
        if (index < dialog.Length - 1)
        {
            index++;
            txtBox[usedTxtBox].text = string.Empty;
            string[] txtSplit = dialog[index].Split("/%");
            if (txtSplit.Length == 1)
            {
                usedTxtBox = 0;
                skippeable = true;
            }
            else
            {
                usedTxtBox = 1;
                skippeable = false;
                dialog[index] = txtSplit[0];
                buttons = new GameObject[txtSplit.Length - 1];
                for(int i = 1; i < txtSplit.Length; i++)
                {
                    buttons[i-1] = Instantiate(btn, txtBox[1].transform);
                    //GameObject btnTemp = Instantiate(btn, txtBox[1].transform);
                    buttons[i-1].GetComponentInChildren<TextMeshProUGUI>().text = txtSplit[i];
                    switch (i)
                    {
                        case 1:
                            buttons[i - 1].GetComponent<Button>().onClick.AddListener(btnAction1);
                            break;
                        case 2:
                            buttons[i - 1].GetComponent<Button>().onClick.AddListener(btnAction2);
                            break;
                    }
                }
            }
            
            StartCoroutine(TypeLine());
        }
        else
        {
            txtBox[usedTxtBox].enabled = false;
            key.SetActive(false);
            near = false;
            dialogBox.SetActive(false);
            inDialog = false;
            SceneController.instance.paused = false;
            Time.timeScale = 1f;
            AudioListener.pause = false;
        }
    }
    private void madFinish()
    {
        GetComponent<Enemy>().hostile = true;
        GetComponent<Collider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Animator>().SetBool("IsAngry", true);
    }
    private void btnAction1()
    {
        print(1);
        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }
        buttons = null;
        nextLine();
    }
    private void btnAction2()
    {
        foreach(GameObject button in buttons)
        {
            Destroy(button);
        }
        buttons = null;
        nextLine();
        madFinish();
    }
}
