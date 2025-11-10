using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 10f;
    public float airTime = 0;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        airTime += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            GameManager.scoreBonus += airTime;
            airTime = 0;
        }
        else
        {
            Destroy(gameObject);
            airTime = 0;
        }
    }
}
