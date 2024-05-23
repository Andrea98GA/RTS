using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    void Die()
    {
        Debug.Log(name + " is dead!");
        Destroy(gameObject);
    }
}