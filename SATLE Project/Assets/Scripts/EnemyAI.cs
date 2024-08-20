using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


// youtube tutorial for AI pathfinding enemies: https://www.youtube.com/watch?v=jvtFUfJ6CP8&t=2s

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    // How close enemy needs to be to desination before picking next one
    public float nextWaypointDistance = 3f;

    // Handle graphics to flip animation based on direction
    public Transform enemyGFX;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Find the components on object
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        // Need to continuously update path based on player's location
        InvokeRepeating("UpdatePath", 0f, .5f); // update every half second
        
    }

    void UpdatePath()
    {
        // Don't want path to update again if still carrying out previous update
        if (seeker.IsDone())
        // Start point of enemy, end of path (target position), function to call when done generating path
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    // Function takes in generated path
    void OnPathComplete(Path p)
    {
        // Check for errors in path generation - if no errors set path to p (generated path)
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // FixedUpdate is called fixed number of times per second
    void FixedUpdate()
    {
        // if path exists, enemy should follow it
        if (path == null)
            return;

        // if all waypoints reached enemy would stop moving - so check for more waypoints
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

        // Enemy movement
        // Get direction of movement to next waypoint
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized; // ensures length of vector is 1

        // Apply force to enemy to make it move in the given direction
        Vector2 force = direction * speed * Time.deltaTime; // time as removes variation caused by framerate

        rb.AddForce(force);
        // The above applies lots of force to enemy, linear drag added in linear to make enemy come to a halt at waypoint

        // get distance to the next way point
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            //current waypoint reached and ready to move to next one
            currentWaypoint++;
        }

        // Considers horizontal movement of the enemy - checks if moving to the right
        if (force.x >= 0.01f)
        {
            // Flips animation for moving to the right
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            // Flips animation for moving to the left
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }

    }
}
