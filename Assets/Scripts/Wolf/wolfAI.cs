using UnityEngine;
using UnityEngine.AI;

public class WolfAI : MonoBehaviour
{
    public Transform player;             
    public float detectionRange = 10f;   
    public float attackRange = 2f;       
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
                
            }
        }
        else
        {
            agent.ResetPath();
            animator.SetBool("isWalking", false);
        }
    }

    
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
        GetComponent<Collider>().enabled = false; 
        
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
                animator.SetTrigger("attack");  
                StartCoroutine(AttackCooldown());
            }
        }
    }

    System.Collections.IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(1.5f); 
        canAttack = true;
    }
}
