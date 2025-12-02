using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayanihanController : MonoBehaviour
{
    [Header("The Load (House)")]
    public Rigidbody2D houseRb; // This is what we check for failure
    public float maxTiltAngle = 45f; 

    [Header("Team Left")]
    public Rigidbody2D leftCarRb;
    public Rigidbody2D leftBackTire;
    public Rigidbody2D leftFrontTire;

    [Header("Team Right")]
    public Rigidbody2D rightCarRb;
    public Rigidbody2D rightBackTire;
    public Rigidbody2D rightFrontTire;

    [Header("Settings")]
    public float speed = 1000f;       // Higher speed for heavy lifting
    public float carTorque = 500f;    // Helps keep them upright
    
    float horizontalInput;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");    
        
        // Check if the HOUSE has tipped over
        CheckForFailure();
    }
    
    private void CheckForFailure()
    {
        // --- YOUR FAILURE LOGIC (Applied to the House) ---
        
        // Get the angle of the HOUSE
        float normalizedAngle = Mathf.Atan2(houseRb.transform.up.y, houseRb.transform.up.x) * Mathf.Rad2Deg - 90;
        
        // Check if the absolute angle exceeds the maximum tilt
        if (Mathf.Abs(normalizedAngle) > maxTiltAngle)
        {
            FailLevel();
        }
    }
    
    private void FailLevel()
    {
        // 1. Stop EVERYTHING (House + Both Teams)
        houseRb.angularVelocity = 0;
        houseRb.linearVelocity = Vector2.zero; // Note: Use 'linearVelocity' if on Unity 6

        // Stop Left Team
        leftCarRb.angularVelocity = 0;
        leftCarRb.linearVelocity = Vector2.zero;
        leftBackTire.angularVelocity = 0;
        leftFrontTire.angularVelocity = 0;

        // Stop Right Team
        rightCarRb.angularVelocity = 0;
        rightCarRb.linearVelocity = Vector2.zero;
        rightBackTire.angularVelocity = 0;
        rightFrontTire.angularVelocity = 0;
        
        // 2. Disable future control input
        this.enabled = false; 
        
        Debug.Log("Game Over! The House fell.");
    }

    private void FixedUpdate()
    {
        // Only allow movement if the script is enabled (i.e., not failed)
        if (this.enabled)
        {
            float dt = Time.fixedDeltaTime;

            // --- APPLY FORCES TO LEFT TEAM ---
            leftBackTire.AddTorque(-horizontalInput * speed * dt);
            leftFrontTire.AddTorque(-horizontalInput * speed * dt);
            leftCarRb.AddTorque(-horizontalInput * carTorque * dt);

            // --- APPLY FORCES TO RIGHT TEAM ---
            rightBackTire.AddTorque(-horizontalInput * speed * dt);
            rightFrontTire.AddTorque(-horizontalInput * speed * dt);
            rightCarRb.AddTorque(-horizontalInput * carTorque * dt);
        }
    }
}