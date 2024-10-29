using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallClosing : MonoBehaviour
{
    public float stretchSpeed = 3f; // Speed at which the wall stretches
    public float maxStretchAmount = 4f; // How much the wall should stretch downward

    private Vector3 initialScale; // Original scale of the wall
    private Vector3 initialPosition; // Original position of the wall
    private bool isStretching = false; // Flag to check if the wall should stretch

    void Start()
    {
        // Save the initial scale and position of the wall
        initialScale = transform.localScale;
        initialPosition = transform.position;
    }

    void Update()
    {
        // If the wall is set to stretch, stretch it downward
        if (isStretching)
        {
            StretchWallDownward();
        }
    }

    void StretchWallDownward()
    {
        // Calculate the new scale and position of the wall
        float newHeight = Mathf.MoveTowards(transform.localScale.y, initialScale.y + maxStretchAmount, stretchSpeed * Time.deltaTime);

        // Set the new scale (stretch the wall on the Y axis)
        transform.localScale = new Vector3(initialScale.x, newHeight, initialScale.z);

        // Adjust the position so the top part of the wall stays in place (move the wall down)
        float newYPosition = initialPosition.y - (newHeight - initialScale.y) / 2;
        transform.position = new Vector3(initialPosition.x, newYPosition, initialPosition.z);

        // Stop stretching when the wall reaches the target stretch amount
        if (transform.localScale.y >= initialScale.y + maxStretchAmount)
        {
            isStretching = false;
        }
    }

    // Method to start the wall stretching
    public void StartStretchingWall()
    {
        isStretching = true;
    }
}