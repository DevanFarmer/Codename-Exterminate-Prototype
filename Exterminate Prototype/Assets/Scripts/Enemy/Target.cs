using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float health = 30f;

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
