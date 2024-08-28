using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCollect : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        ItemCollect itemCollect = collider2D.GetComponent<ItemCollect>();
        if (itemCollect != null)
        {
            Debug.Log("Picking up " + itemCollect.item.name);

            Inventory.instance.Add(item);

            // Destroy the item object
            Destroy(collider2D.gameObject);
        }
    }
}
