using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesInRing : MonoBehaviour
{
    public struct CoordsFlat
    { //custom struct intended for holding a set of two-dimensional coordinates
        public float a; //variables are not named after specific axes as they represent two-dimensional coordinates in three-dimensional space
        public float b;
    }
    private float minSafeDistance = 250f; //inner spawn ring bound
    private float limit = 300f; //outer spawn ring bound
    public float delayBetweenWaves = 2f;
    public static int golemsPerWave = 4;
    public GameObject golemPrefab;
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
    private float GetRandomRadius() //generate random radius in a ring-shaped area
    {
        float inner = Mathf.Max(0f, minSafeDistance); //the closest to the ore pile that the enemy can spawn
        float outer = Mathf.Max(inner, limit); //the farthest from the ore pile that the enemy can spawn
        return (Random.Range(inner, outer)); //a random number between the inner and outer range
    }
    private float GetRandomAngle() //generate random angle between 0 and 360 degrees
    {
        return (Random.Range(0f, Mathf.PI * 2f));
    }
    private CoordsFlat ConvertCoords(float angle, float radius) //convert polar coordinates to cartesian coordinates
    {
        CoordsFlat c;
        c.a = Mathf.Cos(angle) * radius; //X
        c.b = Mathf.Sin(angle) * radius; //Z
        return c;
    }
    public void SpawnWave()
    {
        for (int i = 0; i < golemsPerWave; i++)
        {
            float angle = GetRandomAngle();
            float radius = GetRandomRadius();
            CoordsFlat c = ConvertCoords(angle, radius);
            Vector3 spawnPos = transform.position + new Vector3(c.a, 0, c.b); //set up coordinates
            GameObject newGolem = Instantiate(golemPrefab, spawnPos, Quaternion.identity);
            activeGolems.Add(newGolem);

            StartCoroutine(RemoveWhenDead(newGolem)); //spawn enemy at random location inside spawn ring
        }
    }
    IEnumerator RemoveWhenDead(GameObject golem)
    {
        yield return new WaitUntil(() => golem == null);
        activeGolems.Remove(golem);
    }
}