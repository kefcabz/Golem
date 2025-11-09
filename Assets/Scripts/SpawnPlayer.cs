using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform spawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(player,spawn.position,spawn.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
