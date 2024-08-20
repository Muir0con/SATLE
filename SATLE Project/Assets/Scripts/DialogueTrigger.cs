using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogueScript;

    private bool playerDetected;

    // Detect trigger for dialogue
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if triggered show indicator
        if(collision.tag == "Player")
        {
            playerDetected = true;
            dialogueScript.ToggleIndicator(playerDetected);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If lost trigger - disable playerDetected and hide indicator
        if (collision.tag == "Player")
        {
            playerDetected = false;
            dialogueScript.ToggleIndicator(playerDetected);
            dialogueScript.EndDialogue();

            // Tell dialogue script when player leaves trigger area
            if (dialogueScript.firstDialogueComplete)
            {
                dialogueScript.PlayerExited();
            }
        }
    }


    // While detected if we interact start dialogue
    private void Update()
    {
        if(playerDetected && Input.GetKeyDown(KeyCode.Return))
        {
            dialogueScript.StartDialogue();
        }
    }

}
