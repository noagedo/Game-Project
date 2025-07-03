using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartSceneOnTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // בדיקה האם הדמות נוגעת (בהנחה שהדמות עם תגית "Player")
        if (other.CompareTag("Player"))
        {
            // טען מחדש את הסצנה הנוכחית
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
