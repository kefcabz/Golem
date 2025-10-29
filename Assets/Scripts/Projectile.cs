using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;      
    public float lifetime = 10f;    

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
