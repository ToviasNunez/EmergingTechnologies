using UnityEngine;

public class GoalDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Goal Reached!");
            // Here you can trigger a message or an end-of-level sequence
        }
    }
}
