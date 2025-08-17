using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("3D Tutorial Text")]
    public TextMeshPro tutorialText3D;  

    [Header("2D UI Tutorial Text")]
    public TextMeshProUGUI tutorialSideText2D; 

    private int step = 0;

    void Start()
    {
        
        tutorialText3D.text = "Welcome!\n Help the fairy collect all the crystals\n and avoid the goblins.";
        tutorialText3D.gameObject.SetActive(true);

        tutorialSideText2D.text = "[M] Map\n[Enter] Shoot\n[C] Camera"; 

        Invoke(nameof(StartTutorial), 3f);
    }

    void StartTutorial()
    {
        tutorialText3D.gameObject.SetActive(false);
        ShowStep(1);
    }

    void Update()
    {
        if (step == 1 && Input.GetKeyDown(KeyCode.M))
        {
            ShowStep(2);
        }
        else if (step == 2 && Input.GetKeyDown(KeyCode.Return))
        {
            ShowStep(3);
        }
        else if (step == 3 && Input.GetKeyDown(KeyCode.C))
        {
            Hide3DTutorial();
        }
    }

    void ShowStep(int newStep)
    {
        step = newStep;
        switch (step)
        {
            case 1:
                Show3DText("Press M to open the map.");
                break;
            case 2:
                Show3DText("Press Enter to shoot goblins.");
                break;
            case 3:
                Show3DText("Press C to switch cameras.");
                break;
        }
    }

    void Show3DText(string message)
    {
        tutorialText3D.text = message;
        tutorialText3D.gameObject.SetActive(true);
        CancelInvoke(nameof(Hide3DTutorial));
        Invoke(nameof(Hide3DTutorial), 3f); 
    }

    void Hide3DTutorial()
    {
        tutorialText3D.gameObject.SetActive(false);
    }
}
