using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject[] spawnGameObjects; // Array of game objects to spawn
    [SerializeField] float spawnInterval = 0.5f;   // Time interval between spawns
    [SerializeField] Vector3 minPosition;         // Minimum position for spawning
    [SerializeField] Vector3 maxPosition;         // Maximum position for spawning

    void Start()
    {
        StartCoroutine(BubbleSpawn());
    }

    IEnumerator BubbleSpawn()
    {
        while (true)
        {
            // Generate random positions within the given range for x, y, and z
            float randomX = Random.Range(minPosition.x, maxPosition.x);
            float randomY = Random.Range(minPosition.y, maxPosition.y);
            float randomZ = Random.Range(minPosition.z, maxPosition.z);

            Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);

            // Randomly select a game object from the array to spawn
            GameObject bubble = Instantiate(
                spawnGameObjects[Random.Range(0, spawnGameObjects.Length)],
                spawnPosition,
                Quaternion.identity
            );

            // Destroy the spawned object after 5 seconds
            //Destroy(bubble, 5f);

            // Wait for the specified interval before spawning the next object
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
