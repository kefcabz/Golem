using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    NavMeshAgent golemPatrol;
    public GameObject orePile;
    public Transform targetDirection;
    public GameObject projPrefab;
    public Transform player;
    public float vAngle = 45f;
    public float vDistance = 15f;
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
        Vector3 toPlayer = (player.position - transform.position).normalized;
        float plangle = Vector3.Angle(transform.forward, toPlayer);
        if (plangle < vAngle) {
            if (Vector3.Distance(transform.position, player.position) <= vDistance)
            {
                golemPatrol.SetDestination(player.transform.position);
                TryMurder();
            }
    }
}
    void TryMurder()
    {
        if (projPrefab != null)
        {
            GameObject projectile=Instantiate(projPrefab,targetDirection.position,targetDirection.rotation);
        }
    }

}
