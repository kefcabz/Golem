using UnityEngine;

public class OreHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameManager gameManager; 

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(20);

            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (gameManager != null)
            gameManager.DamageOre(amount);

        if (currentHealth <= 0)
            DestroyOre();
    }

    void DestroyOre()
    {
        Debug.Log("Ore destroyed!");
        Destroy(gameObject);
    }
}
