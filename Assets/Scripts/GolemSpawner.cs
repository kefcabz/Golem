using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class GolemSpawner : MonoBehaviour
{
    public GameObject golemPrefab;
    public int golemsPerWave = 4;
    public float spawnRadius = 70f;
    public float delayBetweenWaves = 2f;

    private List<GameObject> activeGolems = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitUntil(() => activeGolems.Count == 0);
            yield return new WaitForSeconds(delayBetweenWaves);

            SpawnWave();
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < golemsPerWave; i++)
        {
            Vector3 spawnPos = GetRandomPointOnNavMesh(spawnRadius);

            GameObject newGolem = Instantiate(golemPrefab, spawnPos, Quaternion.identity);
            activeGolems.Add(newGolem);

            StartCoroutine(RemoveWhenDead(newGolem));
        }
    }

    Vector3 GetRandomPointOnNavMesh(float radius)
    {
        for (int attempts = 0; attempts < 20; attempts++) 
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position; 

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 50f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        
        return transform.position;
    }

    IEnumerator RemoveWhenDead(GameObject golem)
    {
        yield return new WaitUntil(() => golem == null);
        activeGolems.Remove(golem);
    }
}
