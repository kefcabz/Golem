using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    NavMeshAgent golemPatrol;
    public GameObject orePile;
    public Transform targetDirection;
    public GameObject projPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        golemPatrol = GetComponent<NavMeshAgent>();
        orePile = GameObject.FindGameObjectWithTag("Objective");
        golemPatrol.SetDestination(orePile.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
