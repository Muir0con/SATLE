using UnityEngine;

// youtube tutorial: https://www.youtube.com/watch?v=HQNl3Ff2Lpo

public class Interactible : MonoBehaviour
{
    public float radius = 0.7f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
