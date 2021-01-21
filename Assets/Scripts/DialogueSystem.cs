using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }
    public int PlayerQuestCode;

    public string npcName;
    public List<string> dialogueLines = new List<string>();

    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public TMP_Text nameText;
    public PlayerMovement playerMovement;
    int dialogueIndex;


    //TEXT ANIMATION VARIABLES:
    private string[] charsToShow;
    private string currentText;
    private string textToAnimate;

    private int indexChar = 0;
    private string previousChar;
    private float delayBetweenChars = 0.02f;

    void Awake()
    {
        dialoguePanel.SetActive(false);

        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Update()
    {
        if(dialoguePanel.activeSelf)
        {
            if (Input.GetButtonDown("Submit")) ContinueDialogue();
        }
    }

    public void AddNewDialogue(string[] lines, string npcName)
    {
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);

        this.npcName = npcName;
        CreateDialogue();
    }

    public void CreateDialogue()
    {
        dialoguePanel.SetActive(true);
        nameText.text = npcName;
        AnimateText(0.1f, dialogueLines[dialogueIndex]);
        playerMovement.enabled = false;
        //Play mc stopped animation
        
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count-1)
        {
            dialogueIndex++;

            AnimateText(0.1f, dialogueLines[dialogueIndex]);
        }
        else
        {
            dialoguePanel.SetActive(false);
            playerMovement.enabled = true;
        }
    }


    public void AnimateText(float startDelay, string text)
    {
        textToAnimate = text;
        charsToShow = new string[text.Length];
        for (int i = 0; i < text.Length; i++)
            charsToShow[i] = text[i].ToString();

        dialogueText.text = "";

        StartCoroutine(StartAnimation(startDelay));
    }

    private IEnumerator StartAnimation(float startDelay)
    {
        yield return new WaitForSeconds(startDelay);

        currentText = "";


        previousChar = charsToShow[0];         
        dialogueText.text = charsToShow[0]; //changed indexChar to 0

        indexChar = 1;
        while (indexChar < charsToShow.Length)
        {
            yield return new WaitForSeconds(delayBetweenChars);

            currentText += previousChar + "";
            dialogueText.text = currentText + charsToShow[indexChar];

            previousChar = charsToShow[indexChar];
            indexChar ++;
        }

        yield return new WaitForSeconds(delayBetweenChars);
        dialogueText.text = textToAnimate;
    }
}
