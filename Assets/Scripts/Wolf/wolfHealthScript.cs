using UnityEngine;

public class WolfHealthScript : MonoBehaviour
{
    public int maxHealth = 1;
    private int currentHealth;
    private WolfAI wolfAI;
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        wolfAI = GetComponent<WolfAI>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("The Wolf got heart , life " + currentHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            if (wolfAI != null)
                wolfAI.SendMessage("Die");  
        }
    }
}
