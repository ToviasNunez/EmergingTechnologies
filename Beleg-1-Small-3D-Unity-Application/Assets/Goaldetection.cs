using UnityEngine;

public class GoalDetection : MonoBehaviour
{
    // This script should be attached to the goal object (e.g., a sphere)
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name);  // Log what triggered the event
        
        // Check if the player entered the goal's trigger zone
        if (other.CompareTag("Player"))
        {
            Debug.Log("Goal Reached!");
            // Here you can trigger end-of-level logic, play a sound, or display a victory message
            
        }
    }
}
