using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public Terrain terrain;  
    public float heightOffset = 1f;

    void Start()
    {
        Vector3 terrainSize = terrain.terrainData.size;

        // Pick a random X and Z position 
        float randomX = Random.Range(0, terrainSize.x);
        float randomZ = Random.Range(0, terrainSize.z);

        // Get terrain height at that position
        float terrainHeight = terrain.SampleHeight(new Vector3(randomX, 0, randomZ));

        // Set spawn position
        Vector3 spawnPos = new Vector3(randomX, terrainHeight + heightOffset, randomZ);

        // Move player to new position
        transform.position = spawnPos;
    }
}
