using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    public WallClosing wall; // Reference to the WallMovement script attached to the wall

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collides with the trigger
        if (other.CompareTag("Player"))
        {
            // Start the wall stretching when the player hits the trigger
            wall.StartStretchingWall();
        }
    }
}
