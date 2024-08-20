using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    private bool playerDetected;

    // Detect trigger for dialogue
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if triggered show indicator
        if (collision.tag == "Player")
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If lost trigger - disable playerDetected and hide indicator
        if (collision.tag == "Player")
        {
            playerDetected = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Level 2");
        }

    }
}
