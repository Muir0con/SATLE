using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// youtube tutorial: https://www.youtube.com/watch?v=HQNl3Ff2Lpo


public class Inventory : MonoBehaviour
{
    #region Singleton
    // creating static variable (shared by all instances of a class) called instance
    public static Inventory instance;

    // On start of game setting instance equal to this particular component
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }
    #endregion

    // Delegate for updating inventory UI
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 7;

    public List<Item> items = new List<Item>();
    public bool Add (Item item)
    /*public void Add(Item item)*/
    {
        if (!item.isDefaultItem)
        {
            /*items.Add(item);*/
            if (items.Count >= space)
            {
                Debug.Log("Not enough inventory space");
                return false;
            }

            items.Add(item);

            // Update inventory UI
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        return true;

    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

}
