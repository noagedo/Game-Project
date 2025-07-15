using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FairyHealthScript : MonoBehaviour
{
    public int maxLives = 3;
    private int livesLeft;

    private Animator animator;
    private bool isDead = false;
    private FairyAnimationController movementScript;

    public TeleportPoint[] teleportPoints;
    private TeleportPoint lastTeleportPoint;

    public Slider healthSlider;
    public Text healthText;

    void Start()
    {
        livesLeft = maxLives;
        animator = GetComponent<Animator>();
        movementScript = GetComponent<FairyAnimationController>();

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxLives;
            healthSlider.value = livesLeft;
        }

        if (healthText != null)
            healthText.text = $"life: {livesLeft}/{maxLives}";

        if (teleportPoints == null || teleportPoints.Length == 0)
            teleportPoints = FindObjectsOfType<TeleportPoint>();

        lastTeleportPoint = new GameObject("InitialTeleportPoint").AddComponent<TeleportPoint>();
        lastTeleportPoint.transform.position = transform.position;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        livesLeft -= damage;
        Debug.Log("הפיה נפגעה! חיים נותרו: " + livesLeft);

        if (healthSlider != null)
            healthSlider.value = livesLeft;

        if (healthText != null)
            healthText.text = $"life: {livesLeft}/{maxLives}";

        if (livesLeft <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TeleportPoint tp = other.GetComponent<TeleportPoint>();
        if (tp != null)
        {
            lastTeleportPoint = tp;
            Debug.Log("הפיה נכנסה לטלפורט: " + tp.name);
        }
    }

    void Die()
    {
        if (isDead) return; // הגנה כפולה

        isDead = true;
        Debug.Log("Fairy Die");

        animator.SetBool("isDead", true);
        if (movementScript != null)
            movementScript.enabled = false;

        Invoke(nameof(TeleportToLastPoint), 2f);
    }


    void TeleportToLastPoint()
    {
        Debug.Log("מנסה לטלפורט ל: " + (lastTeleportPoint != null ? lastTeleportPoint.name : "NULL"));

        if (lastTeleportPoint != null)
        {
            transform.position = lastTeleportPoint.transform.position;
        }
        else
        {
            transform.position = Vector3.zero;
        }

        livesLeft = maxLives;

        if (healthSlider != null)
            healthSlider.value = livesLeft;

        if (healthText != null)
            healthText.text = $"life: {livesLeft}/{maxLives}";

        animator.SetBool("isDead", false);
        if (movementScript != null)
            movementScript.enabled = true;
        isDead = false;
    }


    void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
