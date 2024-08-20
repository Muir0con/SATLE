using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// youtube tutorial: https://www.youtube.com/watch?v=JfyWgJBShak
public class Dialogue : MonoBehaviour
{
    //Fields
    //Window
    public GameObject window;
    //Indicator
    public GameObject indicator;

    // Text component
    public TMP_Text dialogueText;

    //Dialogues list
    public List<string> dialogues;
    public List<string> questionDialogue = new List<string>()
    {
        "Have you returned with an item?\nYes (Y)     No (N)"
    };

    // Write speed
    public float writingSpeed;

    //Index on dialogue
    private int index;

    //Character index
    private int charIndex;

    //Indicate started boolean
    private bool started;

    //Wait for next bool
    private bool waitForNext;

    // Track completion of first dialogue
    public bool firstDialogueComplete = false;

    // Track yes/no response
    private bool waitResponse = false;

    // Track if player left trigger area after first dialogue
    private bool playerExitedAfterFirstDialogue = false;


    private void Awake()
    {
        ToggleIndicator(false);
        ToggleWindow(false);
    }


    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }

    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    //Start Dialogue
    public void StartDialogue()
    {
        if (started)
            return;
        //set start boolean to indicate dialogue has started
        started = true;

        //Show window
        ToggleWindow(true);

        //hide indicator
        ToggleIndicator(false);

        // Start correct dialogue
        if (!firstDialogueComplete)
        {
            //First Dialogue
            GetDialogue(0);
        }
        // Only start question dialogue after player leaves and comes back to trigger area
        else if (playerExitedAfterFirstDialogue)
        {
            StartQuestionDialogue();
        }

        
    }


    private void GetDialogue(int i)
    {
        //start index at zero
        index = i;

        //Reset character Index
        charIndex = 0;
        // clear dialogue text
        dialogueText.text = string.Empty;

        //start writing
        StartCoroutine(Writing());
    }


    //End Dialogue
    public void EndDialogue()
    {
        // Disable started
        started = false;

        // Disable wait for next
        waitForNext = false;

        StopAllCoroutines();

        //Hide window
        ToggleWindow(false);

        if (!firstDialogueComplete)
        {
            firstDialogueComplete = true;
        }
    }


    // Writing
    IEnumerator Writing()
    {
        yield return new WaitForSeconds(writingSpeed);

        string currentDialogue = dialogues[index];

        //Write character
        dialogueText.text += currentDialogue[charIndex];

        //Inrease character index to access next character
        charIndex++;

        // Check for end of sentence - then wait for player input to start next sentence
        if(charIndex < currentDialogue.Length)
        {
            // wait - interval between writing each character
            yield return new WaitForSeconds(writingSpeed);

            // restart same process
            StartCoroutine(Writing());
        }
        else
        {
            // End sentence and wait for next
            waitForNext = true;
        }

        
    }


    private void Update()
    {
        if (!started)
            return;

        if(waitResponse)
        {
            HandleYesNoInput();
            return;
        }

        if(waitForNext && Input.GetKeyDown(KeyCode.Return))
        {
            waitForNext = false;

            index++;

            // Check if in scope of dialogues list
            if(index < dialogues.Count)
            {
                //Fetch next dialogue
                GetDialogue(index);
            }
            else
            {
                // end dialogue process
                ToggleIndicator(true);
                EndDialogue();
            }

            GetDialogue(index);
        }
    }


    private void StartQuestionDialogue()
    {
        waitResponse = true;
        dialogueText.text = questionDialogue[0];
    }

    private void HandleYesNoInput()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            waitResponse = false;

            // Yes response
            dialogueText.text = questionDialogue[1];
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            waitResponse = false;
            // No response
            dialogueText.text = questionDialogue[2];
        }
        else if (Input.GetKeyDown(KeyCode.Return) && !waitResponse)
        {
            EndDialogue();
        }
    }

    // Call when player leaves trigger area
    public void PlayerExited()
    {
        playerExitedAfterFirstDialogue = true;
    }

}
