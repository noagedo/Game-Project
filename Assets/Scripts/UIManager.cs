using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text crystalsText;
    public Slider healthSlider;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateCrystalsUI(GameManager.crystals);
        UpdateHealthUI(GameManager.lives);
    }

    public void UpdateCrystalsUI(int amount)
    {
        if (crystalsText != null)
            crystalsText.text = "Crystals: " + amount;
    }

    public void UpdateHealthUI(int amount)
    {
        if (healthSlider != null)
            healthSlider.value = amount;
    }
}
