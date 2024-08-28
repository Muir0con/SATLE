using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// youtube tutorial: https://www.youtube.com/watch?v=HQNl3Ff2Lpo

public class ItemCollect : MonoBehaviour
{
    public Item item; 

    // Method triggered whenever player collider enters another collider where is trigger checkmark is true
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        // Only execute the following when tag of object is coin
        if (collider2D.gameObject.CompareTag("Coin"))
        //if (collider2D.gameObject.CompareTag("Player"))
        {
            Debug.Log("Picking up " + item.name);

            // Add to inventory
            bool wasPickedUp = Inventory.instance.Add(item);
            /*Inventory.instance.Add(item);*/

            if (wasPickedUp)
                Destroy(collider2D.gameObject);
        }
    }
}
