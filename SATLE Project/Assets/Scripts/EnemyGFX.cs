using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;

    // Update is called once per frame
    void Update()
    {
        // Considers horizontal movement of the enemy - checks if moving to the right
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            // Flips animation for moving to the right
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            // Flips animation for moving to the left
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
