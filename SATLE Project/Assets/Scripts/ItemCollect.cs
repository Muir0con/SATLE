using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    // Method triggered whenever player collider enters another collider where is trigger checkmark is true
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        // Only execute the following when tag of object is coin
        if (collider2D.gameObject.CompareTag("Coin"))
        {
            Destroy(collider2D.gameObject);
        }
    }
}
