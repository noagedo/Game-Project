using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Canvas quitMenu;
    public Button startText;
    public Button exitText;

    void Start()
    {
        quitMenu.enabled = false;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ReturnToMainMenu();
        }
    }

    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }

    public void noPress()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }
    public void StartLevel()
    {
        SceneManager.LoadScene("SceneOne");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }


}
