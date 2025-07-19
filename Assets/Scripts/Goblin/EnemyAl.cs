using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform pointA;                
    public Transform pointB;                
    public float patrolSpeed = 2f;          
    public float chaseSpeed = 4f;         
    public float chaseRange = 8f;           
    public float attackRange = 2f;          
    public float attackCooldown = 1.5f;     

    private Transform player;
    private Vector3 targetPoint;
    private Animator animator;
    private float lastAttackTime;

    public float waitTimeAtPoint = 2f; 
    private float waitTimer = 0f;
    private bool waiting = false;
    private bool isInBattle = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        targetPoint = pointB.position;
    }

    void Update()
    {

        if (player == null) return;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            if (isInBattle)
            {
                FindObjectOfType<MusicManager>().PlayVillageMusic();
                isInBattle = false;
            }
            Patrol();
        }
    }


    void Patrol()
    {
        animator.SetBool("isMoving", true);
        animator.SetBool("isAttacking", false);

        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTimeAtPoint)
            {
                waiting = false;
                targetPoint = targetPoint == pointA.position ? pointB.position : pointA.position;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, patrolSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
            {
                waiting = true;
                waitTimer = 0f;
            }
        }

        LookAtDirection(targetPoint);
    }


    void ChasePlayer()
    {

        if (!isInBattle)
        {
            FindObjectOfType<MusicManager>().PlayBattleMusic();
            isInBattle = true;
        }
        animator.SetBool("isMoving", true);
        animator.SetBool("isAttacking", false);

        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        LookAtDirection(player.position);
    }

    void AttackPlayer()
    {
        animator.SetBool("isMoving", false);

        // בדיקת פיה בטווח פיזי (מגע)
        Collider[] hitPlayers = Physics.OverlapSphere(transform.position, attackRange);
        bool playerInRange = false;

        foreach (Collider col in hitPlayers)
        {
            if (col.CompareTag("Player"))
            {
                playerInRange = true;

                if (Time.time - lastAttackTime > attackCooldown)
                {
                    animator.SetBool("isAttacking", true);
                    lastAttackTime = Time.time;

                    FairyHealthScript fairyHealth = col.GetComponent<FairyHealthScript>();
                    if (fairyHealth != null)
                    {
                        fairyHealth.TakeDamage(1);
                    }

                    Debug.Log("The goblin attacked the fairy!");
                }

                break; // תוקפים רק פעם אחת
            }
        }

        if (!playerInRange)
        {
            animator.SetBool("isAttacking", false); // לא תוקף אם אין פיה בטווח
        }

        LookAtDirection(player.position);
    }

    void LookAtDirection(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        direction.y = 0; 
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5f);
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");
       
        Destroy(gameObject, 2f); 
    }
}
