using UnityEngine;
using UnityEngine.AI;

public class ChaseEnemy : MonoBehaviour
{
    public Transform player; 
    public float chaseRange = 10f; 
    public float attackRange = 2f; 

    private NavMeshAgent agent;
    private Animator animator;

    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chaseRange)
        {
            isChasing = true;
        }

        if (isChasing)
        {
            agent.SetDestination(player.position);

            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed); 

            if (distance <= attackRange)
            {
                agent.isStopped = true;
                animator.SetBool("IsAttacking", true);
            }
            else
            {
                agent.isStopped = false;
                animator.SetBool("IsAttacking", false);
            }
        }
    }

    public int health = 100;
    private bool isDead = false;

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        agent.isStopped = true;
        animator.SetBool("IsDead", true);
        
        Destroy(gameObject, 5f);
    }

}
