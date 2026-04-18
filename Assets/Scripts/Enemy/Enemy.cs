using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public float health = 50f;

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
            Die();
    }

    private void Die()
    {
        // TODO: Add death animation, sound, particles.
        Destroy(gameObject);
    }
}