using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class FairyHealthScript : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private Animator animator;
    private bool isDead = false;
    private FairyAnimationController movementScript;

    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        movementScript = GetComponent<FairyAnimationController>();

        
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("הפיה נפגעה! חיים נותרו: " + currentHealth);

        
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("הפיה מתה!");

        animator.SetBool("isDead", true);

        if (movementScript != null)
            movementScript.enabled = false;

        Invoke("GameOver", 2f);
    }

    void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
