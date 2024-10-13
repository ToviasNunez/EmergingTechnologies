using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    public GameObject[] collectablePrefabs;  // Array of different collectable prefabs
    public int numberOfCollectables = 20;    // Total number of collectables to spawn

    public Transform platformA;  // Reference to Platforms_A
    public Transform platformB;  // Reference to Platforms_B

    // Define the sizes of the platforms based on their scale in the scene
    public Vector3 platformASize = new Vector3(10, 1, 10);  // Adjust to match Platforms_A
    public Vector3 platformBSize = new Vector3(15, 1, 15);  // Adjust to match Platforms_B

    void Start()
    {
        if (collectablePrefabs == null || collectablePrefabs.Length == 0)
        {
            Debug.LogError("No collectable prefabs assigned!");
            return;
        }

        if (platformA == null || platformB == null)
        {
            Debug.LogError("One or both platforms (Platforms_A or Platforms_B) are not assigned!");
            return;
        }

        SpawnCollectables();
    }

    void SpawnCollectables()
    {
        for (int i = 0; i < numberOfCollectables; i++)
        {
            // Randomly choose a platform to spawn on (50% chance for each)
            bool spawnOnPlatformA = Random.value > 0.5f;

            // Set platform and size based on which platform was chosen
            Transform chosenPlatform = spawnOnPlatformA ? platformA : platformB;
            Vector3 platformSize = spawnOnPlatformA ? platformASize : platformBSize;

            // Choose a random collectable prefab from the array
            int randomIndex = Random.Range(0, collectablePrefabs.Length);
            GameObject collectableToSpawn = collectablePrefabs[randomIndex];

            // Generate a random position within the chosen platform's area
            Vector3 randomPosition = new Vector3(
                Random.Range(-platformSize.x / 2, platformSize.x / 2),
                chosenPlatform.position.y + 0.5f,  // Set higher to spawn above the platform
                Random.Range(-platformSize.z / 2, platformSize.z / 2)
            );

            // Offset the position by the platform's position in the scene
            randomPosition += chosenPlatform.position;

            // Instantiate the collectable at the random position
            Instantiate(collectableToSpawn, randomPosition, Quaternion.identity);
        }
    }

    // Optional: To visualize the spawn areas for both platforms in the Scene view
    private void OnDrawGizmosSelected()
    {
        if (platformA != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(platformA.position, platformASize);
        }

        if (platformB != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(platformB.position, platformBSize);
        }
    }
}
