using UnityEngine;

public class ChaseAndAttackAI : MonoBehaviour
{
    public Transform player;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float chaseRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;

    private Vector3 patrolPoint;
    public Transform pointA;
    public Transform pointB;

    private Animator animator;
    private float lastAttackTime;

    public float waitTimeAtPoint = 2f;  // זמן המתנה בכל נקודה
    private float waitTimer = 0f;
    private bool waiting = false;
    private bool isInBattle = false;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }
        animator = GetComponent<Animator>();
        patrolPoint = pointB.position;
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
        animator.SetBool("isMoving", !waiting);
        animator.SetBool("isAttacking", false);

        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTimeAtPoint)
            {
                waiting = false;
                patrolPoint = patrolPoint == pointA.position ? pointB.position : pointA.position;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoint, patrolSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, patrolPoint) < 0.1f)
            {
                waiting = true;
                waitTimer = 0f;
            }
        }

        LookAtDirection(patrolPoint);
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

        if (Time.time - lastAttackTime > attackCooldown)
        {
            animator.SetBool("isAttacking", true);
            lastAttackTime = Time.time;

            FairyHealthScript fairyHealth = player.GetComponent<FairyHealthScript>();
            if (fairyHealth != null)
            {
                fairyHealth.TakeDamage(1);
            }

            Debug.Log("Enemy attacks the fairy!");
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
