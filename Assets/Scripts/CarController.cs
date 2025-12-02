using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody2D frontTireRb, backTireRb, carRb;
    public float speed = 15f; 
    public float carTorque = 20f; 
    public float maxTiltAngle = 45f; // <--- NEW: Maximum angle (in degrees) before failure
    
    float horizontalInput;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");    
        
        // --- NEW: Failure Check ---
        CheckForFailure();
    }
    
    private void CheckForFailure()
    {
        // Get the current Z-rotation of the Bahay Kubo body (carRb)
        float currentAngle = carRb.rotation; 
        
        // Normalize the angle to be between -180 and 180 degrees
        // This makes it easier to check against positive/negative limits.
        // float normalizedAngle = currentAngle % 360; // Simple modulo for continuous rotation
        
        // A more robust way to handle continuous rotation for angle checking:
        float normalizedAngle = Mathf.Atan2(carRb.transform.up.y, carRb.transform.up.x) * Mathf.Rad2Deg - 90;
        
        // Check if the absolute angle exceeds the maximum tilt
        if (Mathf.Abs(normalizedAngle) > maxTiltAngle)
        {
            // The Bahay Kubo has tipped over!
            FailLevel();
        }
    }
    
    private void FailLevel()
    {
        // 1. Stop all movement
        carRb.angularVelocity = 0;
        carRb.linearVelocity = Vector2.zero;
        frontTireRb.angularVelocity = 0;
        backTireRb.angularVelocity = 0;
        
        // 2. Disable future control input
        this.enabled = false; // Stops the Update and FixedUpdate calls
        
        // 3. (Optional) Visual/Audio Feedback
        Debug.Log("Game Over! The Bayanihan team failed to keep the Bahay Kubo balanced.");
        
        // In a real game, you would display a Game Over UI here, restart the scene, etc.
        // Example: SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    private void FixedUpdate()
    {
        // Only allow movement if the script is enabled (i.e., not failed)
        if (this.enabled)
        {
            // ... (Your existing movement code) ...
            backTireRb.AddTorque(-horizontalInput * speed * Time.fixedDeltaTime);
            frontTireRb.AddTorque(-horizontalInput * speed * Time.fixedDeltaTime);
            carRb.AddTorque(-horizontalInput * carTorque * Time.fixedDeltaTime);
        }
    }
}