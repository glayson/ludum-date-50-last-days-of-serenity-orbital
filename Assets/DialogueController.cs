using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour
{

    public List<string> introDialogue;
    public TextMeshProUGUI text;
    int indice = 0;
    public int stage = 0;
    public Animator anim;
    public GameObject startPanel;
    public GameObject dialoguePanel;
    public SerenityHealth serenity;
    public GameObject gameUi;

    // Start is called before the first frame update
    void Start()
    {
        startPanel.SetActive(true);
        dialoguePanel.SetActive(false);
        text.text = introDialogue[indice];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && stage == 0)
        {
            stage++;
            startPanel.SetActive(false);
            dialoguePanel.SetActive(true);
            anim.SetTrigger("StartIntro");
        }
        if (Input.anyKeyDown && stage == 5)
        {
            ResetGame();
        }
        if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")) && stage == 1)
        {
            Debug.Log(indice);
            indice++;
            if (indice < introDialogue.Count)
            {
                text.text = introDialogue[indice];
            }
            else
            {
                stage++;
                dialoguePanel.SetActive(false);
                gameUi.SetActive(true);
                anim.SetTrigger("StartGame");
                serenity.gameIsRunning = true;
            }
        }
    }
    void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}