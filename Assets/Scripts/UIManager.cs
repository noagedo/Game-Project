using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Assign once on a persistent Canvas")]
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

    void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        UpdateCrystalsUI(GameManager.crystals);          // מצטבר
        UpdateHealthUI(GameManager.lives, GameManager.maxLives);
    }

    void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        // לרענן UI בכל סצנה (במיוחד אם הערכים השתנו בזמן טעינה)
        UpdateCrystalsUI(GameManager.crystals);
        UpdateHealthUI(GameManager.lives, GameManager.maxLives);
    }

    public void UpdateCrystalsUI(int amount)
    {
        if (crystalsText != null)
            crystalsText.text = "Crystals: " + amount;   // מצטבר
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