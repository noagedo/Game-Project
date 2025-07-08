using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static int crystals = 0;
    public static int lives = 3;
    public static int maxLives = 3; 

    public static int score = 0;

    public static void AddCrystals(int amount)
    {
        crystals += amount;
        Debug.Log("נקודות גבישים: " + crystals);
        if (UIManager.Instance != null)
            UIManager.Instance.UpdateCrystalsUI(crystals);
    }

    public static void LoseLife(int amount)
    {
        lives -= amount;
        Debug.Log("חיים נותרו: " + lives);
        if (UIManager.Instance != null)
            UIManager.Instance.UpdateHealthUI(lives, maxLives); // ✅ שלח גם את maxLives

        if (lives <= 0)
        {
            Debug.Log("Game Over!");
            // כאן אפשר לעשות מעבר סצנה או הפעלה מחדש
        }
    }

    public static void AddPoints(int points)
    {
        score += points;
        Debug.Log("Points: " + score);
    }
}
