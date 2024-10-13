using UnityEngine;

public class Collectable : MonoBehaviour
{
    // This function is called when another object enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collectable picked up!");
            
            // Destroy the collectable after it is collected
            Destroy(gameObject);
        }
    }
}
