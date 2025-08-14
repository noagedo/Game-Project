using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static int crystals = 0;   // מצטבר בין סצנות
    public static int lives = 3;
    public static int maxLives = 3;
    public static int score = 0;

    private static int remainingCrystals; // כמה נשאר לאסוף בסצנה הנוכחית

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // בתחילת כל סצנה מאפסים רק את המונה של מה שנשאר בסצנה
        remainingCrystals = 0;

        // רענון UI עם הערכים המצטברים
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateCrystalsUI(crystals);
            UIManager.Instance.UpdateHealthUI(lives, maxLives);
        }
    }

    // קריסטל נרשם כשהוא נטען (Awake של הקריסטל)
    public static void RegisterCrystal()
    {
        remainingCrystals++;
        // אופציונלי: Debug.Log("Registered crystal. Remaining in scene: " + remainingCrystals);
    }

    // נקרא כשקריסטל נאסף בפועל
    public static void UnregisterCrystal()
    {
        remainingCrystals--;
        // אופציונלי: Debug.Log("Unregistered crystal. Remaining in scene: " + remainingCrystals);

        if (remainingCrystals <= 0)
        {
            Debug.Log("All Crystal Picked up!");
            LoadNextOrVictoryScene();
        }
    }

    public static void AddCrystals(int amount)
    {
        crystals += amount; // מצטבר
        Debug.Log("Total Crystals Collected (cumulative): " + crystals);

        if (UIManager.Instance != null)
            UIManager.Instance.UpdateCrystalsUI(crystals);
    }

    public static void LoseLife(int amount)
    {
        lives -= amount;
        Debug.Log("Life: " + lives);

        if (UIManager.Instance != null)
            UIManager.Instance.UpdateHealthUI(lives, maxLives);

        if (lives <= 0)
        {
            Debug.Log("Game Over!");
            // טפל במסך Game Over אם תרצה
        }
    }

    public static void AddPoints(int points)
    {
        score += points;
        Debug.Log("Points: " + score);
    }

    private static void LoadNextOrVictoryScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 3) // כמו אצלך
            SceneManager.LoadScene("VictoryScene");
        else
            SceneManager.LoadScene(currentSceneIndex + 1);
    }
}