using UnityEngine;

public class GoblinHealthScript : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;
    private EnemyAI enemyAI;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        if (enemyAI != null) enemyAI.enabled = false;
        Debug.Log("Goblin die");
        Destroy(gameObject, 2f); 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }
}
