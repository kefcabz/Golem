using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    NavMeshAgent golemPatrol;
    public GameObject orePile;
    public Transform targetDirection;
    public GameObject projPrefab;
    public float patrolSpeed = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        golemPatrol = GetComponent<NavMeshAgent>();
        orePile = GameObject.FindGameObjectWithTag("Objective");
        golemPatrol.SetDestination(orePile.transform.position);
        golemPatrol.speed = patrolSpeed;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
