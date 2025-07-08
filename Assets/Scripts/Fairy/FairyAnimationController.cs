using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FairyAnimationController : MonoBehaviour
{
    public Animator animator;
    public float speed = 5f;
    public float gravity = -9.81f;
    public float attackRange = 2f;
    public LayerMask enemyLayer;
    public float acceleration = 5f;

    private Vector3 currentDirection = Vector3.zero;

    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocity;

    private bool isClimbing = false;
    public float climbSpeed = 3f;
    public bool isFirstPerson = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (animator == null) animator = GetComponent<Animator>();
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {

        if (animator.GetBool("isDead")) return;
        if (isClimbing)
        {
            ClimbLadder();
            return;
        }

        Vector3 inputDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) inputDirection += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) inputDirection += Vector3.back;
        if (Input.GetKey(KeyCode.A)) inputDirection += Vector3.left;
        if (Input.GetKey(KeyCode.D)) inputDirection += Vector3.right;

        if (Input.GetKey(KeyCode.Q)) inputDirection += Vector3.forward + Vector3.left;
        if (Input.GetKey(KeyCode.E)) inputDirection += Vector3.forward + Vector3.right;
        if (Input.GetKey(KeyCode.Z)) inputDirection += Vector3.back + Vector3.left;
        if (Input.GetKey(KeyCode.X)) inputDirection += Vector3.back + Vector3.right;

        inputDirection = inputDirection.normalized;

        Vector3 targetDirection = Vector3.zero;

        if (inputDirection != Vector3.zero)
        {
            // כיוון התנועה לפי המצלמה (Third Person)
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            targetDirection = camForward * inputDirection.z + camRight * inputDirection.x;
            targetDirection.Normalize();

            // תנועה חלקה לכיוון המטרה
            currentDirection = Vector3.Lerp(currentDirection, targetDirection, Time.deltaTime * acceleration);

            controller.Move(currentDirection * speed * Time.deltaTime);

            // סיבוב חלק לכיוון התנועה
            Quaternion toRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }
        else
        {
            // אם אין תנועה – האטה הדרגתית
            currentDirection = Vector3.Lerp(currentDirection, Vector3.zero, Time.deltaTime * acceleration);
            controller.Move(currentDirection * speed * Time.deltaTime);
        }

        // גרביטציה
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        animator.SetFloat("Speed", currentDirection.magnitude);

        // התקפה
        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("isAttacking");
            TryAttack();
        }
    }

    void ClimbLadder()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 climbMovement = new Vector3(0, vertical * climbSpeed * Time.deltaTime, 0);
        transform.position += climbMovement;

        animator.SetBool("isClimbing", vertical != 0);
        velocity.y = 0;
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
                Debug.Log("Fairy attacked Goblin");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
            animator.SetBool("isClimbing", true);
            controller.enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
            animator.SetBool("isClimbing", false);
            controller.enabled = true;
        }
    }
}
