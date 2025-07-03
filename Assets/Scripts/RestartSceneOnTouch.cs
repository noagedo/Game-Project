using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartSceneOnTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // ����� ��� ����� ����� (����� ������ �� ���� "Player")
        if (other.CompareTag("Player"))
        {
            // ��� ���� �� ����� �������
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
