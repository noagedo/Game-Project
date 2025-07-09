using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text crystalsText;
    public Slider healthSlider;
    public Text healthText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateCrystalsUI(GameManager.crystals);
        UpdateHealthUI(GameManager.lives, GameManager.maxLives);
    }

    public void UpdateCrystalsUI(int amount)
    {
        if (crystalsText != null)
            crystalsText.text = "Crystals: " + amount;
    }

    public void UpdateHealthUI(int current, int max)
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = max;
            healthSlider.value = current;
        }

        if (healthText != null)
            healthText.text = "life: " + current + "/" + max;
    }
}
