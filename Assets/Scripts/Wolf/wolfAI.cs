using UnityEngine;
using UnityEngine.AI;

public class WolfAI : MonoBehaviour
{
    public Transform player;             // הפיה
    public float detectionRange = 10f;   // מתי הזאב מתחיל לזוז
    public float attackRange = 2f;       // מתי הוא תוקף
    public float maxHealth = 100f;
    private bool canAttack = true;


    private float currentHealth;
    private bool isDead = false;

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < detectionRange)
        {
            agent.SetDestination(player.position);
            animator.SetBool("isWalking", true);

            if (distance < attackRange)
            {
                animator.SetTrigger("attack");
                // כאן אפשר להוסיף נזק לשחקנית
            }
        }
        else
        {
            agent.ResetPath();
            animator.SetBool("isWalking", false);
        }
    }

    // קריאה כשפוגעים בזאב (למשל מקסם של הפיה)
    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        agent.isStopped = true;
        GetComponent<Collider>().enabled = false; // לא חובה אבל מונע פגיעות נוספות
        // אפשר גם: Destroy(gameObject, 5f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Player") && canAttack)
        {
            FairyHealthScript fairyHealth = other.GetComponent<FairyHealthScript>();
            if (fairyHealth != null)
            {
                fairyHealth.TakeDamage(1);
                animator.SetTrigger("attack");  // תפעיל גם את אנימציית התקיפה
                StartCoroutine(AttackCooldown());
            }
        }
    }

    System.Collections.IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(1.5f); // זמן המתנה בין תקיפות
        canAttack = true;
    }
}
