using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static int crystals = 0;   
    public static int lives = 3;
    public static int maxLives = 3;
    public static int score = 0;

    private static int remainingCrystals; 

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
        
        remainingCrystals = 0;

        
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateCrystalsUI(crystals);
            UIManager.Instance.UpdateHealthUI(lives, maxLives);
        }
    }

    
    public static void RegisterCrystal()
    {
        remainingCrystals++;
       
    }

   
    public static void UnregisterCrystal()
    {
        remainingCrystals--;
        

        if (remainingCrystals <= 0)
        {
            Debug.Log("All Crystal Picked up!");
            LoadNextOrVictoryScene();
        }
    }

    public static void AddCrystals(int amount)
    {
        crystals += amount; 
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

        if (currentSceneIndex == 3) 
            SceneManager.LoadScene("VictoryScene");
        else
            SceneManager.LoadScene(currentSceneIndex + 1);
    }
}