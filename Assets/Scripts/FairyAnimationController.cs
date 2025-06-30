using UnityEngine;

public class FairyAnimationController : MonoBehaviour
{
    public Animator animator;
    public float speed = 5f;
    public float attackRange = 2f;
    public LayerMask enemyLayer;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (controller == null)
        {
            Debug.LogError("CharacterController component is missing on " + gameObject.name);
            enabled = false;
            return;
        }

        if (animator == null)
        {
            Debug.LogError("Animator component is missing on " + gameObject.name);
        }
        else
        {
            Debug.Log("Animator found on: " + animator.gameObject.name);
        }
    }

    void Update()
    {
        if (!enabled) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, 0, v);

        if (direction.magnitude > 0.1f)
        {
            controller.Move(direction.normalized * speed * Time.deltaTime);
            transform.forward = direction.normalized;
        }

        animator.SetFloat("Speed", direction.magnitude);

       
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("isAttacking");
            TryAttack();
        }
    }

    void TryAttack()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        foreach (Collider enemy in enemies)
        {
            GoblinHealthScript enemyHealth = enemy.GetComponent<GoblinHealthScript>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1);
                Debug.Log("פיה תקפה גובלין!");
            }
        }
    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}