using UnityEngine;

// youtube tutorial: https://www.youtube.com/watch?v=HQNl3Ff2Lpo

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use ()
    {
        // Use item

        Debug.Log("Using " + name);
    }

}
