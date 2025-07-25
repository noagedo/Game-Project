using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int crystals = 0;
    public static int lives = 3;
    public static int maxLives = 3;
    public static int score = 0;

    private static int totalCrystalsInScene;

    private void Start()
    {
        // סופרים את כל הקריסטלים עם הטאג "Crystal" בתחילת הסצנה
        totalCrystalsInScene = GameObject.FindGameObjectsWithTag("Crystal").Length;
        crystals = 0;
    }

    public static void AddCrystals(int amount)
    {
        crystals += amount;
        Debug.Log("Score of Crystals: " + crystals);

        if (UIManager.Instance != null)
            UIManager.Instance.UpdateCrystalsUI(crystals);

        
        if (crystals >= totalCrystalsInScene)
        {
            Debug.Log("All Crystal Picked up!");
            LoadNextOrVictoryScene();
        }
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
        {
            SceneManager.LoadScene("VictoryScene"); 
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
